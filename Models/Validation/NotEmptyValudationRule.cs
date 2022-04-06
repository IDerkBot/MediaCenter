using System.Globalization;
using System.Windows.Controls;

namespace MediaCenter.Models.Validation
{
	internal class NotEmptyValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			return string.IsNullOrWhiteSpace((value ?? "").ToString())
				? new ValidationResult(false, "Поле обязательное.")
				: ValidationResult.ValidResult;
		}
	}
}
