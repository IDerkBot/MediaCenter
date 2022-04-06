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
	/// Логика взаимодействия для ServiceTypesPage.xaml
	/// </summary>
	public partial class ServiceTypesPage : Page
	{
		public ServiceTypesPage()
		{
			InitializeComponent();
		}

		private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var search = (sender as TextBox)?.Text.ToLower();
			var list = MediaCenterEntities.GetContext().TypeServices.ToList();
			var searchResult =
				list.Where(x =>
					x.Name.ToLower().Contains(search)
				).ToList();
			DgServiceTypes.ItemsSource = searchResult;
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new TypeServiceEditPage((sender as Button).DataContext as TypeService));

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new TypeServiceEditPage());

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			var items = DgServiceTypes.SelectedItems.Cast<TypeService>().ToList();
			if (!MessageValidation.DeleteItems(items.Count)) return;
			MediaCenterEntities.GetContext().TypeServices.RemoveRange(items);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные удалены!");
			DgServiceTypes.ItemsSource = MediaCenterEntities.GetContext().TypeServices.ToList();
		}

		private void ServiceTypesPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			DgServiceTypes.ItemsSource = MediaCenterEntities.GetContext().TypeServices.ToList();
		}
	}
}
