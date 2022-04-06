using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediaCenter.Models.Entity;

namespace MediaCenter.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для SettingsPage.xaml
	/// </summary>
	public partial class SettingsPage : Page
	{
		public SettingsPage()
		{
			InitializeComponent();
		}

		private void SettingsPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			CbFiles.ItemsSource = MediaCenterEntities.GetContext().Documents.ToList();
			CbFiles.SelectedItem = MediaCenterEntities.GetContext().Documents.Single(x => x.Title == "Оказание услуг");
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
