using JustbokApplication.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class ExpenseType : PropertyChangedNotification
    {
        public int ExpenseTypeId { get { return GetValue(() => ExpenseTypeId); }set { SetValue(() => ExpenseTypeId, value); } }

        [Required(ErrorMessage ="Please provide expense type name")]
        [MaxLength(100,ErrorMessage = "Expense type name exceeded 50 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Expense type name contains invalid characters")]
        public string TypeName { get { return GetValue(() => TypeName); } set { SetValue(() => TypeName, value); } }

        [Required(ErrorMessage = "Please provide expense type description")]
        [MaxLength(200, ErrorMessage = "Expense type description exceeded 500 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Expense type description contains invalid characters")]
        public string TypeDescription { get { return GetValue(() => TypeDescription); } set { SetValue(() => TypeDescription, value); } }

        public bool IsActive { get { return GetValue(() => IsActive); } set { SetValue(() => IsActive, value); } }
        public int BranchId { get { return GetValue(() => BranchId); } set { SetValue(() => BranchId, value); } }
    }
}
