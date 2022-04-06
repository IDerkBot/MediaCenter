using System.Windows;

namespace MediaCenter.Models.Validation
{
	internal class MessageValidation
	{
		public static bool DeleteItems(int count)
		{
			return MessageBox.Show($"Вы действительно хотите удалить {count} элементов?", "Удаление!", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
		}
	}
}
