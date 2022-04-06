using MediaCenter.Models;
using MediaCenter.Models.Entity;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MediaCenter.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для ProfilePage.xaml
	/// </summary>
	public partial class ProfilePage : Page
	{
		private readonly Manager _currentManager;
		public ProfilePage()
		{
			InitializeComponent();
			_currentManager = MediaCenterEntities.GetContext().Managers.Any(x => x.IDUser == Data.UserID)
				? MediaCenterEntities.GetContext().Managers.Single(x => x.IDUser == Data.UserID)
				: new Manager();
			if (_currentManager.ID == 0) _currentManager.IDUser = Data.UserID;
			DataContext = _currentManager;
		}

		private void ProfilePage_OnLoaded(object sender, RoutedEventArgs e)
		{
			if (_currentManager.Image == null) return;
			TbProfileImage.Visibility = Visibility.Collapsed;
			ProfileImage.Visibility = Visibility.Visible;
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			if (_currentManager.ID == 0) MediaCenterEntities.GetContext().Managers.Add(_currentManager);
			MediaCenterEntities.GetContext().SaveChanges();
			PageManager.GoBack();
		}

		private void BImage_OnDrop(object sender, DragEventArgs e)
		{
			var filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (filePath == null) return;
			_currentManager.Image = File.ReadAllBytes(filePath[0]);
			var image = new BitmapImage();
			var ms = new MemoryStream(_currentManager.Image);
			image.BeginInit();
			image.StreamSource = ms;
			image.EndInit();
			ProfileImage.Source = image;
			TbProfileImage.Visibility = Visibility.Collapsed;
			ProfileImage.Visibility = Visibility.Visible;
			//ms.Dispose();
		}
	}
}
