using MediaCenter.Models;
using MediaCenter.Models.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaCenter.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для RegistrationPage.xaml
	/// </summary>
	public partial class RegistrationPage : Page
	{
		public RegistrationPage()
		{
			InitializeComponent();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			var login = TbLogin.Text;
			var password = PbPassword.Password;
			var confirm = PbConfirm.Password;
			if (!MediaCenterEntities.GetContext().Users.Any(x => x.Login == login))
			{
				if (confirm == password)
				{
					MediaCenterEntities.GetContext().Users.Add(new User { Login = login, Password = password, Access = 0 });
					MediaCenterEntities.GetContext().SaveChanges();
					MessageBox.Show("Вы успешно зарегистрировались!");
					PageManager.GoBack();
				}
				else
					MessageBox.Show("Пароли не совпадают");
			}
			else
				MessageBox.Show("Такого пользователя уже существует");
		}
	}
}
