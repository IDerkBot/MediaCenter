namespace MediaCenter.Models
{
	internal class Config
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public bool RememberMe { get; set; }
		public Config()
		{

		}
		public Config(string login, string password, bool remember)
		{
			Login = login;
			Password = password;
			RememberMe = remember;
		}
	}
}