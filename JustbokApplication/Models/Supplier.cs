using JustbokApplication.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class Supplier : PropertyChangedNotification
    {
        public int SupplierId { get { return GetValue(() => SupplierId); }set { SetValue(() => SupplierId, value); } }

        [Required(ErrorMessage ="Please provide company name")]
        [MaxLength(20,ErrorMessage ="Company name exceeded 20 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Company name contains invalid characters")]
        public string CompanyName { get { return GetValue(() => CompanyName); } set { SetValue(() => CompanyName, value); } }

        [Required(ErrorMessage = "Please provide registration no")]
        [MaxLength(20, ErrorMessage = "Registration no exceeded 20 characters")]
        [RegularExpression(@"^(\d)+$", ErrorMessage = "Please provide valid registration no")]
        public string RegistrationNo { get { return GetValue(() => RegistrationNo); } set { SetValue(() => RegistrationNo, value); } }

        [Required(ErrorMessage = "Please provide first name")]
        [MaxLength(50, ErrorMessage = "First name exceeded 50 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "First name contains invalid characters")]
        public string FirstName { get { return GetValue(() => FirstName); } set { SetValue(() => FirstName, value); } }

        [Required(ErrorMessage = "Please provide last name")]
        [MaxLength(50, ErrorMessage = "Last name exceeded 50 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Last name contains invalid characters")]
        public string LastName { get { return GetValue(() => LastName); } set { SetValue(() => LastName, value); } }

        [Required(ErrorMessage = "Please provide email")]
        [MaxLength(100, ErrorMessage = "Email exceeded 100 characters")]
        [ExcludeChar("/,!#$%", ErrorMessage = "Email contains invalid characters")]
        public string Email { get { return GetValue(() => Email); } set { SetValue(() => Email, value); } }

        [Required(ErrorMessage = "Please provide phone no")]
        [MaxLength(10, ErrorMessage = "Phone no exceeded 10 characters")]
        [RegularExpression(@"^(\d)+$", ErrorMessage = "Please provide valid phone no")]
        public string PhoneNo { get { return GetValue(() => PhoneNo); } set { SetValue(() => PhoneNo, value); } }

        [Required(ErrorMessage = "Please provide fax no")]
        [MaxLength(15, ErrorMessage = "Fax no exceeded 15 characters")]
        [RegularExpression(@"^(\d)+$", ErrorMessage = "Please provide valid phone no")]
        public string FaxNo { get { return GetValue(() => FaxNo); } set { SetValue(() => FaxNo, value); } }

        [Required(ErrorMessage = "Please provide address")]
        [MaxLength(300, ErrorMessage = "Address exceeded 300 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Address contains invalid characters")]
        public string Address { get { return GetValue(() => Address); } set { SetValue(() => Address, value); } }

        public int BranchId { get { return GetValue(() => BranchId); } set { SetValue(() => BranchId, value); } }
        public bool IsActive { get { return GetValue(() => IsActive); } set { SetValue(() => IsActive, value); } }
        public string SupplierName { get { return GetValue(() => SupplierName); } set { SetValue(() => SupplierName, value); } }
    }
}
