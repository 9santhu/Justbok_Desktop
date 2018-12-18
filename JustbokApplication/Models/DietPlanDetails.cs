using JustbokApplication.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class DietPlanDetails : PropertyChangedNotification
    {
        public DietPlanDetails()
        {
            this.MealTime = new MealTime();
        }
        public int DetailId { get { return GetValue(() => DetailId); } set { SetValue(() => DetailId, value); } }
        public MealTime MealTime { get { return GetValue(() => MealTime); } set { SetValue(() => MealTime, value); } }

        [MaxLength(50, ErrorMessage = "Exceeded 50 characters")]
        [ExcludeChar("/.,!@#$%=*", ErrorMessage = "Contains invalid characters")]
        public string D_Mon { get { return GetValue(() => D_Mon); } set { SetValue(() => D_Mon, value); } }

        [MaxLength(50, ErrorMessage = "Exceeded 50 characters")]
        [ExcludeChar("/.,!@#$%=*", ErrorMessage = "Contains invalid characters")]
        public string D_Tue { get { return GetValue(() => D_Tue); } set { SetValue(() => D_Tue, value); } }

        [MaxLength(50, ErrorMessage = "Exceeded 50 characters")]
        [ExcludeChar("/.,!@#$%=*", ErrorMessage = "Contains invalid characters")]
        public string D_Wed { get { return GetValue(() => D_Wed); } set { SetValue(() => D_Wed, value); } }

        [MaxLength(50, ErrorMessage = "Exceeded 50 characters")]
        [ExcludeChar("/.,!@#$%=*", ErrorMessage = "Contains invalid characters")]
        public string D_Thu { get { return GetValue(() => D_Thu); } set { SetValue(() => D_Thu, value); } }

        [MaxLength(50, ErrorMessage = "Exceeded 50 characters")]
        [ExcludeChar("/.,!@#$%=*", ErrorMessage = "Contains invalid characters")]
        public string D_Fri { get { return GetValue(() => D_Fri); } set { SetValue(() => D_Fri, value); } }

        [MaxLength(50, ErrorMessage = "Exceeded 50 characters")]
        [ExcludeChar("/.,!@#$%=*", ErrorMessage = "Contains invalid characters")]
        public string D_Sat { get { return GetValue(() => D_Sat); } set { SetValue(() => D_Sat, value); } }

        [MaxLength(50, ErrorMessage = "Exceeded 50 characters")]
        [ExcludeChar("/.,!@#$%=*", ErrorMessage = "Contains invalid characters")]
        public string D_Sun { get { return GetValue(() => D_Sun); } set { SetValue(() => D_Sun, value); } }
        
    }
}
