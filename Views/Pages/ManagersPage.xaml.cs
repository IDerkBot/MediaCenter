using System.Collections.Generic;
using MediaCenter.Models;
using MediaCenter.Models.Entity;
using MediaCenter.Views.Pages.Edit;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MediaCenter.Models.Validation;

namespace MediaCenter.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для ManagersPage.xaml
	/// </summary>
	public partial class ManagersPage : Page
	{
		#region Load

		public ManagersPage() => InitializeComponent();
		private void ManagersPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			DgManagers.ItemsSource = MediaCenterEntities.GetContext().Managers.ToList();
			CbSort.ItemsSource = DgManagers.Columns.Select(x => x.Header).ToList();
		}

		#endregion

		#region Buttons

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new ManagerEditPage());

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new ManagerEditPage((sender as Button)?.DataContext as Manager));

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			var items = DgManagers.SelectedItems.Cast<Manager>().ToList();
			if (!MessageValidation.DeleteItems(items.Count)) return;
			MediaCenterEntities.GetContext().Managers.RemoveRange(items);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные удалены!");
			DgManagers.ItemsSource = MediaCenterEntities.GetContext().Managers.ToList();
		}

		#endregion

		#region Changed

		private void CbSort_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var list = DgManagers.ItemsSource.Cast<Manager>().ToList();
			var orderedList = new List<Manager>();
			switch (CbSort.SelectedItem.ToString())
			{
				case "ФИО":
					orderedList.AddRange(list.OrderBy(x => x.Fullname));
					break;
				case "Телефон":
					orderedList.AddRange(list.OrderBy(x => x.Phone));
					break;
				case "Почта":
					orderedList.AddRange(list.OrderBy(x => x.Email));
					break;
				case "Логин":
					orderedList.AddRange(list.OrderBy(x => x.Email));
					break;
			}
			DgManagers.ItemsSource = orderedList;
		}

		private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var search = (sender as TextBox)?.Text.ToLower();
			var list = MediaCenterEntities.GetContext().Managers.ToList();
			var searchResult =
				list.Where(x =>
					x.Fullname.ToLower().Contains(search) ||
					(!string.IsNullOrWhiteSpace(x.Phone) && x.Phone.Contains(search)) ||
					(!string.IsNullOrWhiteSpace(x.Email) && x.Email.ToLower().Contains(search))
				).ToList();
			DgManagers.ItemsSource = searchResult;
		}

		#endregion
	}
}
