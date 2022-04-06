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
	/// Логика взаимодействия для ServicesPage.xaml
	/// </summary>
	public partial class ServicesPage : Page
	{
		#region Load

		public ServicesPage() => InitializeComponent();
		private void ServicesPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			DgServices.ItemsSource = MediaCenterEntities.GetContext().Services.ToList();
			CbSort.ItemsSource = DgServices.Columns.Select(x => x.Header).ToList();
			CbFilter.ItemsSource = MediaCenterEntities.GetContext().Services.GroupBy(x => x.TypeService1.Name).Select(x => x.Key).ToList();
		}

		#endregion

		#region Buttons

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new ServiceEditPage((sender as Button)?.DataContext as Service));

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new ServiceEditPage());

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			var items = DgServices.SelectedItems.Cast<Service>().ToList();
			if (!MessageValidation.DeleteItems(items.Count)) return;
			MediaCenterEntities.GetContext().Services.RemoveRange(items);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные удалены!");
			DgServices.ItemsSource = MediaCenterEntities.GetContext().Services.ToList();
		}

		#endregion

		#region Change

		private void CbSort_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var list = DgServices.ItemsSource.Cast<Service>().ToList();
			var orderedList = new List<Service>();
			switch (CbSort.SelectedItem.ToString())
			{
				case "Название":
					orderedList.AddRange(list.OrderBy(x => x.Name));
					break;
				case "Стоимость за секунду":
					orderedList.AddRange(list.OrderBy(x => x.Cost));
					break;
				case "Тип":
					orderedList.AddRange(list.OrderBy(x => x.TypeService1.Name));
					break;
			}
			DgServices.ItemsSource = orderedList;
		}

		private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var search = (sender as TextBox)?.Text;
			var searchResult =
				MediaCenterEntities.GetContext().Services.Where(x =>
					x.Name.ToLower().Contains(search) ||
					x.Cost.ToString().Contains(search) ||
					x.TypeService1.Name.ToLower().Contains(search)
				).ToList();
			DgServices.ItemsSource = searchResult;
		}
		private void CbFilter_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			DgServices.ItemsSource = MediaCenterEntities.GetContext().Services
				.Where(x => x.TypeService1.Name == CbFilter.SelectedItem.ToString()).ToList();
		}

		#endregion
	}
}
