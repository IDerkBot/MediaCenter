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
	/// Логика взаимодействия для DocumentTemplatesPage.xaml
	/// </summary>
	public partial class DocumentTemplatesPage : Page
	{
		public DocumentTemplatesPage()
		{
			InitializeComponent();
		}

		private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var search = (sender as TextBox)?.Text.ToLower();
			var list = MediaCenterEntities.GetContext().Documents.ToList();
			var searchResult = list.Where(x => x.Title.ToLower().Contains(search)).ToList();
			DgDocuments.ItemsSource = searchResult;
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new DocumentEditPage((sender as Button)?.DataContext as Document));

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new DocumentEditPage());

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			var items = DgDocuments.SelectedItems.Cast<Document>().ToList();
			if (!MessageValidation.DeleteItems(items.Count)) return;
			MediaCenterEntities.GetContext().Documents.RemoveRange(items);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные удалены!");
			DgDocuments.ItemsSource = MediaCenterEntities.GetContext().Documents.ToList();
		}

		private void DocumentTemplatesPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			DgDocuments.ItemsSource = MediaCenterEntities.GetContext().Documents.ToList();
		}
	}
}
