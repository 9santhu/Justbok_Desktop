using JustbokApplication.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class MealTime : PropertyChangedNotification
    {
        public int MealTimeId { get { return GetValue(() => MealTimeId); } set { SetValue(() => MealTimeId, value); } }

        //[DateTime(ErrorMessage = "Please select meal time")]
        public DateTime MTime { get { return GetValue(() => MTime); } set { SetValue(() => MTime, value); } }

        [Required(ErrorMessage = "Please enter description")]
        [MaxLength(50, ErrorMessage = "Description exceeded 50 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Description contains invalid characters")]
        public string Description { get { return GetValue(() => Description); } set { SetValue(() => Description, value); } }

        public bool IsActive { get { return GetValue(() => IsActive); } set { SetValue(() => IsActive, value); } }
        public int BranchId { get { return GetValue(() => BranchId); } set { SetValue(() => BranchId, value); } }
    }
}
