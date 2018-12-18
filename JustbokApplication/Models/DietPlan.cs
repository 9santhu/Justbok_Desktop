using JustbokApplication.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class DietPlan : PropertyChangedNotification
    {
        public DietPlan()
        {
            this.DietPlanDetails = new List<DietPlanDetails>();
        }
        public int DietPlanId { get { return GetValue(() => DietPlanId); } set { SetValue(() => DietPlanId, value); } }

        [Required(ErrorMessage = "Please enter plan name")]
        [MaxLength(100, ErrorMessage = "Plan name exceeded 100 characters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Plan name contains invalid characters")]
        public string PlanName { get { return GetValue(() => PlanName); } set { SetValue(() => PlanName, value); } }

        public bool IsActive { get { return GetValue(() => IsActive); } set { SetValue(() => IsActive, value); } }
        public int BranchId { get { return GetValue(() => BranchId); } set { SetValue(() => BranchId, value); } }

        public IList<DietPlanDetails> DietPlanDetails { get { return GetValue(() => DietPlanDetails); } set { SetValue(() => DietPlanDetails, value); } }
    }
}
