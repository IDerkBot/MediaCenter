using MediaCenter.Models;
using MediaCenter.Models.Entity;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using DataFormats = System.Windows.DataFormats;
using DragEventArgs = System.Windows.DragEventArgs;
using MessageBox = System.Windows.MessageBox;

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
            CbCustomers.ItemsSource = MediaCenterEntities.GetContext().Customers.ToList();
            CbManagers.ItemsSource = MediaCenterEntities.GetContext().Managers.ToList();
            CbServices.ItemsSource = MediaCenterEntities.GetContext().Services.ToList();
		}

		private void CbCustomers_OnTextInput(object sender, TextCompositionEventArgs e)
		{
			MessageBox.Show($"{CbCustomers.Text}");
		}

		private void OrderEditPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			//CbCustomers.ItemsSource = MediaCenterEntities.GetContext().Customers.ToList();
			//CbManagers.ItemsSource = MediaCenterEntities.GetContext().Managers.ToList();
			//CbServices.ItemsSource = MediaCenterEntities.GetContext().Services.ToList();
		}

		private void BtnSave_OnClick(object sender, RoutedEventArgs e)
		{
			//_currentOrder.IDCustomer = _currentOrder.Customer.ID;
			//_currentOrder.IDManager = _currentOrder.Manager.ID;
			//_currentOrder.IDService = _currentOrder.Service.ID;

			// комментарий

            if (_currentOrder.Duration < 0)
            {
                MessageBox.Show("Установите время больше нуля");
                return;
			}

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
			if (!string.IsNullOrWhiteSpace(TbDuration.Text) && _currentOrder.Service != null && TbDuration.Text.All(char.IsDigit))
			{
				var cost = MediaCenterEntities.GetContext().Services.ToList().Where(x => x.Name == _currentOrder.Service.Name).Select(x => x.Cost).First();
                if (!decimal.TryParse(TbDuration.Text, out var count))
                {
                    MessageBox.Show("Не верное значение");
                    return;
				}
				var sum = cost * count;
				_currentOrder.Sum = sum;
				TbSum.Text = $"Сумма: {_currentOrder.Sum} рублей";
			}
		}
	}
}
