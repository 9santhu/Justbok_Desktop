using JustbokApplication.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class ExpenseMode : PropertyChangedNotification
    {
        public int ExpenseModeId { get { return GetValue(() => ExpenseModeId); }set { SetValue(() => ExpenseModeId, value); } }

        [Required(ErrorMessage ="Please provide expense mode")]
        [MaxLength(50,ErrorMessage = "Expense mode exceeded 50 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Expense mode contains invalid characters")]
        public string ExpenseMod { get { return GetValue(() => ExpenseMod); } set { SetValue(() => ExpenseMod, value); } }

        public bool IsActive { get { return GetValue(() => IsActive); } set { SetValue(() => IsActive, value); } }
    }
}
