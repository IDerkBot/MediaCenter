using MediaCenter.Models;
using MediaCenter.Models.Entity;
using MediaCenter.Models.Validation;
using MediaCenter.Views.Pages.Edit;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaCenter.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для UsersPage.xaml
	/// </summary>
	public partial class UsersPage : Page
	{
		#region Load

		public UsersPage() => InitializeComponent();

		private void UsersPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			var users = MediaCenterEntities.GetContext().Users.ToList();
			users.ForEach(x =>
            {
                if (x.Managers.Count > 0)
                    x.Manager = x.Managers.Single().Fullname;
            });
            DgUsers.ItemsSource = users;
			CbSort.ItemsSource = DgUsers.Columns.Select(x => x.Header).ToList();
			CbFilter.ItemsSource = new List<string> { "Менеджер", "Директор" };
		}

		#endregion

		#region Buttons

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new UserEditPage((sender as Button).DataContext as User));

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new UserEditPage());

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			var items = DgUsers.SelectedItems.Cast<User>().ToList();
			if (!MessageValidation.DeleteItems(items.Count)) return;
			MediaCenterEntities.GetContext().Users.RemoveRange(items);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные удалены!");
			DgUsers.ItemsSource = MediaCenterEntities.GetContext().Users.ToList();
		}

		#endregion

		#region Changed

		private void CbSort_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var list = DgUsers.ItemsSource.Cast<User>().ToList();
			var orderedList = new List<User>();
			switch (CbSort.SelectedItem.ToString())
			{
				case "Логин":
					orderedList.AddRange(list.OrderBy(x => x.Login));
					break;
				case "Пароль":
					orderedList.AddRange(list.OrderBy(x => x.Password));
					break;
				case "Роль":
					orderedList.AddRange(list.OrderBy(x => x.Role));
					break;
			}
			DgUsers.ItemsSource = orderedList;
		}

		private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var search = (sender as TextBox)?.Text.ToLower();
			var list = MediaCenterEntities.GetContext().Users.ToList();
			var searchResult =
				list.Where(x =>
					x.Login.ToLower().Contains(search)
				).ToList();
			DgUsers.ItemsSource = searchResult;
		}
		private void CbFilter_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var list = MediaCenterEntities.GetContext().Users.ToList();
			var filterList = list.Where(x =>
				x.Role == CbFilter.SelectedItem.ToString()).ToList();
			DgUsers.ItemsSource = filterList;
		}

		#endregion
	}
}
