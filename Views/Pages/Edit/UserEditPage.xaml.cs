using MediaCenter.Models;
using MediaCenter.Models.Entity;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MediaCenter.Views.Pages.Edit
{
	/// <summary>
	/// Логика взаимодействия для UserEditPage.xaml
	/// </summary>
	public partial class UserEditPage : Page
	{
		private readonly User _currentUser;
		public UserEditPage(User selectedUser = null)
		{
			InitializeComponent();
			_currentUser = selectedUser ?? new User();
			DataContext = _currentUser;
		}
		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(_currentUser.Login) || string.IsNullOrWhiteSpace(_currentUser.Password))
			{
				MessageBox.Show("Вы не заполнили обязательные поля");
				return;
			}
			if (_currentUser.ID == 0) MediaCenterEntities.GetContext().Users.Add(_currentUser);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные сохранены");
			PageManager.GoBack();
		}

		private void UserEditPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			CbRole.ItemsSource = new List<string>{"Менеджер", "Директор"};
		}

		private void CbRole_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_currentUser.Access = CbRole.SelectedItem.ToString() == "Менеджер" ? (byte) 0 : (byte) 1;
		}
	}
}
