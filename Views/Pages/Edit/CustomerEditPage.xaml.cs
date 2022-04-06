using MediaCenter.Models;
using MediaCenter.Models.Entity;
using System.Windows;
using System.Windows.Controls;

namespace MediaCenter.Views.Pages.Edit
{
	/// <summary>
	/// Логика взаимодействия для CustomerEditPage.xaml
	/// </summary>
	public partial class CustomerEditPage : Page
	{
		private readonly Customer _currentCustomer;
		public CustomerEditPage(Customer selectedCustomer = null)
		{
			InitializeComponent();
			_currentCustomer = selectedCustomer ?? new Customer();
			DataContext = _currentCustomer;
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(_currentCustomer.Surname) || string.IsNullOrWhiteSpace(_currentCustomer.Name) ||
			    string.IsNullOrWhiteSpace(_currentCustomer.Patronymic))
			{
				MessageBox.Show("Вы не заполнили обязательные поля");
				return;
			}
			if (_currentCustomer.ID == 0) MediaCenterEntities.GetContext().Customers.Add(_currentCustomer);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные сохранены");
			PageManager.GoBack();
		}
	}
}
