using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace MediaCenter.Models.Validation
{
	internal class PhoneValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			var val = value as string;
			return !string.IsNullOrEmpty(val)
				? val.Any(char.IsLetter) ? new ValidationResult(false, "Телефон может содержать только цифры") : ValidationResult.ValidResult
				: ValidationResult.ValidResult;
		}
	}
}