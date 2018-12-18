using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.CustomValidationAttributes
{
    public class DateTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            //check for null
            if (value == null)
                return new ValidationResult("Please select date");

            //check date
            if (!(value is DateTime))
            {
                //check string
                string val = value as String;
                if (String.IsNullOrEmpty(val))
                    return new ValidationResult("Please select date"); ;

                DateTime dt;
                bool isValid = DateTime.TryParse(val, out dt);
                if (isValid && dt.Year.Equals(0001))
                    return new ValidationResult("Please select date");
                else
                    return ValidationResult.Success;
            }
            else if (((DateTime)value).Year.Equals(0001))
                return new ValidationResult("Please select date");
            else
                return ValidationResult.Success;
        }
    }
}
