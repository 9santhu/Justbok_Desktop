using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace JustbokApplication.CustomValidationRules
{
    public class CurrencyValidationRule : ValidationRule
    {
        private string _errorMessage;

        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                var match = Regex.Match(value.ToString(),@"^(\d)*(\.\d{1,2})?$",RegexOptions.IgnoreCase);

                if (!match.Success)
                {
                    return new ValidationResult(false, ErrorMessage);
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
