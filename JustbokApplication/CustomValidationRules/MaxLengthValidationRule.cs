using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace JustbokApplication.CustomValidationRules
{
    public class MaxLengthValidationRule : ValidationRule
    {
        private int _maxCharacters;
        private string _errorMessage;

        public int MaxCharacters { get => _maxCharacters; set => _maxCharacters = value; }
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                if(value.ToString().Length>MaxCharacters)
                {
                    return new ValidationResult(false, ErrorMessage);
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
