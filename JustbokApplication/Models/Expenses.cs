using JustbokApplication.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class Expenses : PropertyChangedNotification
    {
        public Expenses()
        {
            this.ExpenseDate = DateTime.Now;
        }
        public int ExpenseId { get { return GetValue(() => ExpenseId); } set { SetValue(() => ExpenseId, value); } }

        [Required(ErrorMessage = "Please provide expense type")]
        public ExpenseType ExpenseType { get { return GetValue(() => ExpenseType); } set { SetValue(() => ExpenseType, value); } }

        [DateTime(ErrorMessage = "Please select expense date")]
        public DateTime ExpenseDate { get { return GetValue(() => ExpenseDate); } set { SetValue(() => ExpenseDate, value); } }

        [Required(ErrorMessage = "Please provide expense amount")]
        [RegularExpression(@"^(\d)*(\.\d{1,2})?$", ErrorMessage = "Please provide valid expense amount")]
        public string ExpenseAmount { get { return GetValue(() => ExpenseAmount); } set { SetValue(() => ExpenseAmount, value); } }

        [Required(ErrorMessage = "Please provide expense mode")]
        public ExpenseMode ExpenseMode { get { return GetValue(() => ExpenseMode); } set { SetValue(() => ExpenseMode, value); } }

        [MaxLength(50, ErrorMessage = "Reference number exceeded 50 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Reference number contains invalid characters")]
        public string ReferenceNumber { get { return GetValue(() => ReferenceNumber); } set { SetValue(() => ReferenceNumber, value); } }

        [MaxLength(150, ErrorMessage = "Expense description exceeded 150 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Expense description contains invalid characters")]
        public string ExpenseDescription { get { return GetValue(() => ExpenseDescription); } set { SetValue(() => ExpenseDescription, value); } }

        public bool IsActive { get { return GetValue(() => IsActive); } set { SetValue(() => IsActive, value); } }
        public int BranchId { get { return GetValue(() => BranchId); } set { SetValue(() => BranchId, value); } }


        public IList<ExpenseType> ExpenseTypes { get { return GetValue(() => ExpenseTypes); } set { SetValue(() => ExpenseTypes, value); } }
        public IList<ExpenseMode> ExpenseModes { get { return GetValue(() => ExpenseModes); } set { SetValue(() => ExpenseModes, value); } }
    }
}
