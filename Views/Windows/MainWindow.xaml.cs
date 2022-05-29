using MediaCenter.Models;
using MediaCenter.Views.Pages;
using System;
using System.Windows;

namespace MediaCenter.Views.Windows
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			PageManager.SetFrame(MainFrame);
			PageManager.Navigate(new AuthorizationPage());
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			if (PageManager.GetPage() is OrderInfoPage page)
			{
				page.Stop();
				PageManager.GoBack();
			}
			else
				PageManager.GoBack();
		}

		private void MainFrame_OnContentRendered(object sender, EventArgs e)
		{

		}

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            if (PageManager.GetPage() is OrderInfoPage page)
                page.Stop();
        }
    }
}
