using MediaCenter.Models.Entity;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MediaCenter.Models;

namespace MediaCenter.Views.Pages.Edit
{
	/// <summary>
	/// Логика взаимодействия для OrderEditPage.xaml
	/// </summary>
	public partial class OrderEditPage : Page
	{
		private readonly Order _currentOrder;
		public OrderEditPage(Order selectedOrder = null)
		{
			InitializeComponent();
			_currentOrder = selectedOrder ?? new Order();
			if(selectedOrder == null) _currentOrder.Date = DateTime.Now;
			DataContext = _currentOrder;
		}

		private void CbCustomers_OnTextInput(object sender, TextCompositionEventArgs e)
		{
			MessageBox.Show($"{CbCustomers.Text}");
		}

		private void OrderEditPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			CbCustomers.ItemsSource = MediaCenterEntities.GetContext().Customers.ToList();
			CbManagers.ItemsSource = MediaCenterEntities.GetContext().Managers.ToList();
			CbServices.ItemsSource = MediaCenterEntities.GetContext().Services.ToList();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			_currentOrder.IDCustomer = _currentOrder.Customer.ID;
			_currentOrder.IDManager = _currentOrder.Manager.ID;
			_currentOrder.IDService = _currentOrder.Service.ID;
			if (_currentOrder.ID == 0) MediaCenterEntities.GetContext().Orders.Add(_currentOrder);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные сохранены!");
			PageManager.GoBack();
		}

		private void UIElement_OnDrop(object sender, DragEventArgs e)
		{
			var filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (filePath == null) return;
			TbFileName.Text = $"Файл: {filePath[0].Split('\\').ToList().Last()}";
			_currentOrder.Sound = File.ReadAllBytes(filePath[0]);
		}

		private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TbDuration.Text) && _currentOrder.Service != null)
			{
				var cost = MediaCenterEntities.GetContext().Services.ToList().Where(x => x.Name == _currentOrder.Service.Name).Select(x => x.Cost).First();
				var count = decimal.Parse(TbDuration.Text);
				var list = (cost * count).ToString().ToList();
				list.ForEach(x =>
				{
					if (x == '.') x = ',';
				});
				var sum = string.Join("", list);
				_currentOrder.Sum = (decimal?) double.Parse(sum);
				TbSum.Text = $"Сумма: {_currentOrder.Sum} рублей";
			}
		}
	}
}
