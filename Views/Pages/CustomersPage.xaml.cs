using System.Collections.Generic;
using MediaCenter.Models;
using MediaCenter.Models.Entity;
using MediaCenter.Models.Validation;
using MediaCenter.Views.Pages.Edit;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaCenter.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для CustomersPage.xaml
	/// </summary>
	public partial class CustomersPage : Page
	{
		public CustomersPage()
		{
			InitializeComponent();
		}

		private void BtnEdit_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new CustomerEditPage((sender as Button)?.DataContext as Customer));

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			PageManager.Navigate(new CustomerEditPage());
		}

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			var items = DgCustomers.SelectedItems.Cast<Customer>().ToList();
			if (!MessageValidation.DeleteItems(items.Count)) return;
			MediaCenterEntities.GetContext().Customers.RemoveRange(items);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные удалены!");
			DgCustomers.ItemsSource = MediaCenterEntities.GetContext().Customers.ToList();
		}

		private void CustomersPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			DgCustomers.ItemsSource = MediaCenterEntities.GetContext().Customers.ToList();
			CbSort.ItemsSource = DgCustomers.Columns.Select(x => x.Header).ToList();
			BtnDelete.Visibility = Data.IsDirector ? Visibility.Visible : Visibility.Collapsed;
		}

		private void CbSort_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var list = DgCustomers.ItemsSource.Cast<Customer>().ToList();
			var orderedList = new List<Customer>();
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
			}
			DgCustomers.ItemsSource = orderedList;
		}

		private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var search = (sender as TextBox)?.Text.ToLower();
			var list = MediaCenterEntities.GetContext().Customers.ToList();
			var searchResult =
				list.Where(x =>
					x.Fullname.ToLower().Contains(search) || 
					(!string.IsNullOrWhiteSpace(x.Phone) && x.Phone.Contains(search)) ||
					(!string.IsNullOrWhiteSpace(x.Email) && x.Email.ToLower().Contains(search))
				).ToList();
			DgCustomers.ItemsSource = searchResult;
		}
	}
}
