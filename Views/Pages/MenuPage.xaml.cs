using MediaCenter.Models;
using System.Windows;
using System.Windows.Controls;

namespace MediaCenter.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для MenuPage.xaml
	/// </summary>
	public partial class MenuPage : Page
	{
		public MenuPage() => InitializeComponent();
		private void BtnCustomersMove_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new CustomersPage());
		private void BtnManagersMove_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new ManagersPage());
		private void BtnOrdersMove_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new OrdersPage());
		private void BtnServicesMove_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new ServicesPage());
		private void BtnUsersMove_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new UsersPage());
		private void BtnServiceTypesMove_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new ServiceTypesPage());
		private void BtnProfileMove_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new ProfilePage());
		private void BtnDocumentTemplatesMove_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new DocumentTemplatesPage());
		private void BtnSettingsMove_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new SettingsPage());
		private void MenuPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			BtnProfileMove.Visibility = Data.IsManager ? Visibility.Visible : Visibility.Collapsed;
			BtnUsersMove.Visibility = Data.IsDirector ? Visibility.Visible : Visibility.Collapsed;
			BtnManagersMove.Visibility = Data.IsDirector ? Visibility.Visible : Visibility.Collapsed;
			BtnDocumentTemplatesMove.Visibility = Data.IsDirector ? Visibility.Visible : Visibility.Collapsed;
			BtnSettingsMove.Visibility = Data.IsDirector ? Visibility.Visible : Visibility.Collapsed;
		}
	}
}