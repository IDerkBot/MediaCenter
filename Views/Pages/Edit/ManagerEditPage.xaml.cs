using System.Linq;
using MediaCenter.Models;
using MediaCenter.Models.Entity;
using System.Windows;
using System.Windows.Controls;

namespace MediaCenter.Views.Pages.Edit
{
	/// <summary>
	/// Логика взаимодействия для ManagerEditPage.xaml
	/// </summary>
	public partial class ManagerEditPage : Page
	{
		private readonly Manager _currentManager;
		public ManagerEditPage(Manager selectedManager = null)
		{
			InitializeComponent();
			_currentManager = selectedManager ?? new Manager();
			DataContext = _currentManager;
		}
		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(_currentManager.Surname) || string.IsNullOrWhiteSpace(_currentManager.Name) ||
			    string.IsNullOrWhiteSpace(_currentManager.Patronymic))
			{
				MessageBox.Show("Вы не заполнили обязательные поля");
				return;
			}
			if (_currentManager.ID == 0) MediaCenterEntities.GetContext().Managers.Add(_currentManager);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные сохранены");
			PageManager.GoBack();
		}

		private void ManagerEditPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			CbUsers.ItemsSource = MediaCenterEntities.GetContext().Users.ToList();
		}
	}
}