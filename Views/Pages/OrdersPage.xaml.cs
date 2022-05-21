using System.Collections.Generic;
using MediaCenter.Models;
using MediaCenter.Models.Entity;
using MediaCenter.Models.Validation;
using MediaCenter.Views.Pages.Edit;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MediaCenter.Views.Windows;

namespace MediaCenter.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для OrdersPage.xaml
	/// </summary>
	public partial class OrdersPage : Page
	{
		public OrdersPage()
		{
			InitializeComponent();
		}

		private void CbSort_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var list = DgOrders.ItemsSource.Cast<Order>().ToList();
			var orderedList = new List<Order>();
			switch (CbSort.SelectedItem.ToString())
			{
				case "Номер":
					orderedList.AddRange(list.OrderBy(x => x.ID));
					break;
				case "Заказчик":
					orderedList.AddRange(list.OrderBy(x => x.Customer.Fullname));
					break;
				case "Менеджер":
					orderedList.AddRange(list.OrderBy(x => x.Manager.Fullname));
					break;
				case "Услуга":
					orderedList.AddRange(list.OrderBy(x => x.Service.Name));
					break;
				case "Дата":
					orderedList.AddRange(list.OrderBy(x => x.Date));
					break;
				case "Сумма":
					orderedList.AddRange(list.OrderBy(x => x.Sum));
					break;
			}
			DgOrders.ItemsSource = orderedList;
		}

		private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var search = (sender as TextBox)?.Text.ToLower();
			var list = MediaCenterEntities.GetContext().Orders.ToList();
			var searchResult =
				list.Where(x =>
					x.Customer.Fullname.ToLower().Contains(search) ||
					x.Manager.Fullname.ToLower().Contains(search) ||
					x.Service.Name.ToLower().Contains(search)
				).ToList();
			DgOrders.ItemsSource = searchResult;
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new OrderEditPage((sender as Button)?.DataContext as Order));

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new OrderEditPage());

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			var items = DgOrders.SelectedItems.Cast<Order>().ToList();
			if (!MessageValidation.DeleteItems(items.Count)) return;
			MediaCenterEntities.GetContext().Orders.RemoveRange(items);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные удалены!");
			DgOrders.ItemsSource = MediaCenterEntities.GetContext().Orders.ToList();
		}

		private void BtnMore_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new OrderInfoPage((sender as Button)?.DataContext as Order));

		private void OrdersPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			DgOrders.ItemsSource = MediaCenterEntities.GetContext().Orders.ToList();

			var sortList = DgOrders.Columns.Select(x => x.Header).ToList();
			sortList.Remove(sortList.Last());
			sortList.Remove(sortList.Last());
			CbSort.ItemsSource = sortList;

			//if (MediaCenterEntities.GetContext().Managers.Any(x => x.IDUser == Data.UserID))
			//{
			//	BtnEdit.Visibility = Visibility.Visible;
			//	BtnAdd.Visibility = Visibility.Visible;
			//}
			//else
			//{
			//	BtnEdit.Visibility = Visibility.Collapsed;
			//	BtnAdd.Visibility = Visibility.Collapsed;
			//}
			BtnReport.Visibility = Data.IsDirector ? Visibility.Visible : Visibility.Collapsed;
		}

        private void BtnReport_OnClick(object sender, RoutedEventArgs e)
        {
            new ReportWindow().Show();
        }
    }
}
