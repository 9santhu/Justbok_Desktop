using JustbokApplication.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class Task : PropertyChangedNotification
    {
        public Task()
        {
            //this.IntervalType = new IntervalType();
            this.IntervalTypes = new List<IntervalType>();
        }
        public int TaskId { get { return GetValue(() => TaskId); }set { SetValue(() => TaskId, value); } }

        [Required(ErrorMessage = "Please provide title")]
        [MaxLength(100, ErrorMessage = "Title exceeded 100 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Title contains invalid characters")]
        public string Title { get { return GetValue(() => Title); } set { SetValue(() => Title, value); } }

        [Required(ErrorMessage = "Please provide description")]
        [MaxLength(100, ErrorMessage = "Description exceeded 500 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Description contains invalid characters")]
        public string Description { get { return GetValue(() => Description); } set { SetValue(() => Description, value); } }

        [Required(ErrorMessage = "Please provide description")]
        [RegularExpression(@"^(\d)+$", ErrorMessage = "Please provide valid interval")]
        public int Interval { get { return GetValue(() => Interval); } set { SetValue(() => Interval, value); } }

        [Required(ErrorMessage = "Please select interval type")]
        public IntervalType IntervalType { get { return GetValue(() => IntervalType); } set { SetValue(() => IntervalType, value); } }

        [DateTime(ErrorMessage = "Please select start date")]
        public DateTime StartDate { get { return GetValue(() => StartDate); } set { SetValue(() => StartDate, value); } }

        public bool IsActive { get { return GetValue(() => IsActive); } set { SetValue(() => IsActive, value); } }
        public int StaffId { get { return GetValue(() => StaffId); } set { SetValue(() => StaffId, value); } }
        public int BranchId { get { return GetValue(() => BranchId); } set { SetValue(() => BranchId, value); } }


        public IList<IntervalType> IntervalTypes { get { return GetValue(() => IntervalTypes); } set { SetValue(() => IntervalTypes, value); } }


        
    }
}
