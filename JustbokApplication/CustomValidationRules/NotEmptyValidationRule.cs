using System.Globalization;
using System.Windows.Controls;

namespace JustbokApplication.CustomValidationRules
{
	public class NotEmptyValidationRule : ValidationRule
	{
        private string _errorMessage;

        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			return string.IsNullOrWhiteSpace((value ?? "").ToString())
				? new ValidationResult(false, ErrorMessage)
				: ValidationResult.ValidResult;
		}
	}
}
