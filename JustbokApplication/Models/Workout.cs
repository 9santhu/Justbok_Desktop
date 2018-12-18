using JustbokApplication.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class Workout : PropertyChangedNotification
    {
        public Workout()
        {
            this.Units = new Data.UnitDao().GetAllActiveUnits();
        }
        public int WorkoutId { get { return GetValue(() => WorkoutId); } set { SetValue(() => WorkoutId, value); } }

        [Required(ErrorMessage = "Please enter description")]
        [MaxLength(100, ErrorMessage = "Description exceeded 100 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Description contains invalid characters")]
        public string Description { get { return GetValue(() => Description); } set { SetValue(() => Description, value); } }

        [Required(ErrorMessage = "Please select unit")]
        public Unit Unit { get { return GetValue(() => Unit); } set { SetValue(() => Unit, value); } }

        public bool IsActive { get { return GetValue(() => IsActive); } set { SetValue(() => IsActive, value); } }
        public int BranchId { get { return GetValue(() => BranchId); } set { SetValue(() => BranchId, value); } }

        public IList<Unit> Units { get { return GetValue(() => Units); } set { SetValue(() => Units, value); } }
    }
}
