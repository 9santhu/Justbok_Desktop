using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace JustbokApplication.CustomValidationRules
{
    public class ExcludeCharValidationRule : ValidationRule
    {
        private string _characters;
        private string _errorMessage;

        public string Characters { get => _characters; set => _characters = value; }
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                for (int i = 0; i < Characters.Length; i++)
                {
                    var valueAsString = value.ToString();
                    if (valueAsString.Contains(Characters[i]))
                    {
                        return new ValidationResult(false, ErrorMessage);
                    }
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
