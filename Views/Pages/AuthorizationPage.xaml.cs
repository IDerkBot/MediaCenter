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
using MediaCenter.Models;
using MediaCenter.Models.Entity;

namespace MediaCenter.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для AuthorizationPage.xaml
	/// </summary>
	public partial class AuthorizationPage : Page
	{
		public AuthorizationPage()
		{
			InitializeComponent();
		}

		private void BtnAuth_OnClick(object sender, RoutedEventArgs e)
		{
			var login = TbLogin.Text;
			var password = PbPassword.Password;
			if (MediaCenterEntities.GetContext().Users.Any(x => x.Login == login))
			{
				var user = MediaCenterEntities.GetContext().Users.Single(x => x.Login == login);
				if (user.Password == password)
				{
					Data.UserID = user.ID;
					Data.Access = user.Access;
					if(IsRemember.IsChecked == true) FileManager.SetConfig(new Config(user.Login, user.Password, true));
					PageManager.Navigate(new MenuPage());
				}
				else
					MessageBox.Show("Пароль не верный");
			}
			else
				MessageBox.Show("Такого пользователя не существует");
		}

		private void BtnRegistrationMove_OnClick(object sender, RoutedEventArgs e) =>
			PageManager.Navigate(new RegistrationPage());

		private void AuthorizationPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			var config = FileManager.GetConfig();
			if (!config.RememberMe) return;
			TbLogin.Text = config.Login;
			PbPassword.Password = config.Password;
			IsRemember.IsChecked = true;
		}
	}
}
