using JustbokApplication.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class Package : PropertyChangedNotification
    {
        public Package()
        {
            this.Categories = new List<Category>();
        }
        public int PackageId { get { return GetValue(() => PackageId); }set { SetValue(() => PackageId, value); } }

        [Required(ErrorMessage = "Please provide name")]
        [MaxLength(100, ErrorMessage = "Name exceeded 100 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Name contains invalid characters")]
        public string Name { get { return GetValue(() => Name); } set { SetValue(() => Name, value); } }

        [Required(ErrorMessage = "Please provide months")]
        [RegularExpression(@"^(\d)+$", ErrorMessage = "Please provide valid months")]
        public string Months { get { return GetValue(() => Months); } set { SetValue(() => Months, value); } }

        [Required(ErrorMessage = "Please provide amount")]
        [RegularExpression(@"^(\d)*(\.\d{1,2})?$", ErrorMessage = "Please provide valid amount")]
        public string Amount { get { return GetValue(() => Amount); } set { SetValue(() => Amount, value); } }

        [Required(ErrorMessage = "Please provide minimum amount")]
        [RegularExpression(@"^(\d)*(\.\d{1,2})?$", ErrorMessage = "Please provide valid minimum amount")]
        public string MinAmount { get { return GetValue(() => MinAmount); } set { SetValue(() => MinAmount, value); } }

        [Required(ErrorMessage = "Please select category")]
        public Category Category { get { return GetValue(() => Category); } set { SetValue(() => Category, value); } }

        public bool IsActive { get { return GetValue(() => IsActive); } set { SetValue(() => IsActive, value); } }
        public int StaffId { get { return GetValue(() => StaffId); } set { SetValue(() => StaffId, value); } }
        public int BranchId { get { return GetValue(() => BranchId); } set { SetValue(() => BranchId, value); } }


        public IList<Category> Categories { get { return GetValue(() => Categories); } set { SetValue(() => Categories, value); } }


        
    }
}
