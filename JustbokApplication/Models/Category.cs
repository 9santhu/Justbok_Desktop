using JustbokApplication.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class Category : PropertyChangedNotification
    {
        public int CategoryId { get { return GetValue(() => CategoryId); }set { SetValue(() => CategoryId, value); } }

        [Required(ErrorMessage ="Please provide category name")]
        [MaxLength(100,ErrorMessage ="Category name exceeded 100 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Category name contains invalid characters")]
        public string CategoryName { get { return GetValue(() => CategoryName); } set { SetValue(() => CategoryName, value); } }

        [Required(ErrorMessage = "Please provide Description")]
        [MaxLength(200, ErrorMessage = "Description exceeded 200 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Description contains invalid characters")]
        public string Description { get { return GetValue(() => Description); } set { SetValue(() => Description, value); } }

        public bool IsActive { get { return GetValue(() => IsActive); } set { SetValue(() => IsActive, value); } }
    }
}
