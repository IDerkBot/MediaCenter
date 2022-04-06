namespace MediaCenter.Models
{
	internal class Data
	{
		public static int UserID { get; set; }
		public static int Access { get; set; }
		public static bool IsManager => Access == 0;
		public static bool IsDirector => Access == 1;
		public static string CurrentDirectory { get; set; } = "MediaCenter";
		public static string CurrentConfigFile { get; set; } = "config";
		public static string CurrentConfigExtension { get; set; } = "json";
	}
}
