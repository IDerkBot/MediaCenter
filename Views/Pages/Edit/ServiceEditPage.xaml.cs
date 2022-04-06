using MediaCenter.Models;
using MediaCenter.Models.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaCenter.Views.Pages.Edit
{
	/// <summary>
	/// Логика взаимодействия для ServiceEditPage.xaml
	/// </summary>
	public partial class ServiceEditPage : Page
	{
		private readonly Service _currentService;
		public ServiceEditPage(Service selectedService = null)
		{
			InitializeComponent();
			_currentService = selectedService ?? new Service();
			DataContext = _currentService;
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(_currentService.Name))
			{
				MessageBox.Show("Вы не заполнили обязательные поля");
				return;
			}
			if (_currentService.ID == 0) MediaCenterEntities.GetContext().Services.Add(_currentService);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные сохранены");
			PageManager.GoBack();
		}

		private void ServiceEditPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			CbTypes.ItemsSource = MediaCenterEntities.GetContext().TypeServices.ToList();
		}
	}
}
