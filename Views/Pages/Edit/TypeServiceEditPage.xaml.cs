using MediaCenter.Models;
using MediaCenter.Models.Entity;
using System.Windows;
using System.Windows.Controls;

namespace MediaCenter.Views.Pages.Edit
{
	/// <summary>
	/// Логика взаимодействия для TypeServiceEditPage.xaml
	/// </summary>
	public partial class TypeServiceEditPage : Page
	{
		private readonly TypeService _typeService;
		public TypeServiceEditPage(TypeService selectedService = null)
		{
			InitializeComponent();
			_typeService = selectedService ?? new TypeService();
			DataContext = _typeService;
		}
		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(_typeService.Name))
			{
				MessageBox.Show("Вы не заполнили название");
				return;
			}

			if (_typeService.ID == 0) MediaCenterEntities.GetContext().TypeServices.Add(_typeService);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные сохранены");
			PageManager.GoBack();
		}
	}
}
