using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace MediaCenter.Models.Validation
{
	internal class EmailValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			var val = value as string;
			return !string.IsNullOrEmpty(val)
				? !val.Contains('@') ? new ValidationResult(false, "Почта введена не корректно") : ValidationResult.ValidResult
				: ValidationResult.ValidResult;
		}
	}
}