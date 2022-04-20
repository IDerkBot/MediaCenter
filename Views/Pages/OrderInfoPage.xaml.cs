using MediaCenter.Models;
using MediaCenter.Models.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using word = Microsoft.Office.Interop.Word;

namespace MediaCenter.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для OrderInfoPage.xaml
	/// </summary>
	public partial class OrderInfoPage : Page
	{
		private readonly Order _currentOrder;
		public OrderInfoPage(Order selectedOrder)
		{
			InitializeComponent();
			_currentOrder = selectedOrder;
			DataContext = _currentOrder;
		}

		private bool _isPlay;
		private void BtnPlayPause_OnClick(object sender, RoutedEventArgs e)
		{
			if (_isPlay)
			{
				Pause();
				_isPlay = false;
			}
			else
			{
				Play();
				_isPlay = true;
			}
		}

		private void OrderInfoPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			File.WriteAllBytes($"{folder}/{Data.CurrentDirectory}/orderMusic.mp3", _currentOrder.Sound);
			
			var uri = new Uri($"{folder}/{Data.CurrentDirectory}/orderMusic.mp3", UriKind.Absolute);
			_player.Open(uri);

			var info = TagLib.File.Create($"{folder}/{Data.CurrentDirectory}/orderMusic.mp3");
			var time = info.Properties.Duration.Minutes * 60 + info.Properties.Duration.Seconds;
			SPosition.Maximum = time;
			_player.Volume = .5;
		}

		private readonly MediaPlayer _player = new MediaPlayer();
		private readonly Timer _timer = new Timer();
		private void Play()
		{
			_player.Play();
			_timer.Interval = 1000;
			_timer.Elapsed += TimerOnElapsed;
			_timer.Start();
			IPlay.Visibility = Visibility.Collapsed;
			IPause.Visibility = Visibility.Visible;
		}

		private void TimerOnElapsed(object sender, ElapsedEventArgs e)
		{
			Dispatcher.Invoke(() =>
			{
				var position = _player.Position.Minutes * 60 + _player.Position.Seconds;
				SPosition.Value = position;
			});
		}

		private void Pause()
		{
			_player.Pause();
			IPlay.Visibility = Visibility.Visible;
			IPause.Visibility = Visibility.Collapsed;
		}
		internal void Stop()
		{
			if(_player.CanPause)
				_player.Stop();
			DeleteFile();
		}

		private static void DeleteFile()
		{
			var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{Data.CurrentDirectory}/orderMusic.mp3";
			File.Delete(path);
		}

		private void SVolume_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			_player.Volume = SVolume.Value;
		}

		private bool _moveTrack;
		private void SPosition_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (!_moveTrack) return;
			var i2 = int.Parse(Math.Truncate(SPosition.Value / 60).ToString()); // Получаем количество минут !
			var i1 = int.Parse(Math.Truncate(SPosition.Value % 60).ToString());

			_player.Position = new TimeSpan(0, 0, i2, i1);
		}

		private void SPosition_OnMouseEnter(object sender, MouseEventArgs e)
		{
			_moveTrack = true;
		}

		private void SPosition_OnMouseLeave(object sender, MouseEventArgs e)
		{
			_moveTrack = false;
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			var culture = new CultureInfo("ru-RU");
			var items = new Dictionary<string, string>
			{
				["<Number>"] = _currentOrder.ID.ToString(),
				["<City>"] = "Лукоянов",
				["<Manager>"] = _currentOrder.Manager.Fullname,
				["<Client>"] = _currentOrder.Customer.Fullname,
				["<Date>"] = _currentOrder.Date.ToString(culture.DateTimeFormat.ShortDatePattern),
				["<Service>"] = _currentOrder.Service.Name,
				["<Sum>"] = _currentOrder.Sum.ToString(),
			};
			var app = new word.Application();
			var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			File.WriteAllBytes($"{folder}/{Data.CurrentDirectory}/document.docx",
				MediaCenterEntities.GetContext().Documents.First().File);
			try
			{
				object file = new FileInfo($"{folder}/{Data.CurrentDirectory}/document.docx").FullName;
				var missing = Type.Missing;
				app.Documents.Open(file);
				foreach (var item in items)
				{
					var find = app.Selection.Find;
					find.Text = item.Key;
					find.Replacement.Text = item.Value;
					object wrap = word.WdFindWrap.wdFindContinue;
					object replace = word.WdReplace.wdReplaceAll;

					find.Execute( Type.Missing, false, false, false, missing, false, true, wrap, false, missing, replace );
				}
				app.ActiveDocument.SaveAs2(
					$"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/{_currentOrder.ID}-{_currentOrder.Date.ToString(culture.DateTimeFormat.ShortDatePattern)}.docx");
				app.ActiveDocument.Close();
				
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
				throw;
			}
			finally
			{
				app.Quit();
				File.Delete($"{folder}/{Data.CurrentDirectory}/document.docx");
				Process.Start(
					$"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/{_currentOrder.ID}-{_currentOrder.Date.ToString(culture.DateTimeFormat.ShortDatePattern)}.docx");
			}
		}
	}
}