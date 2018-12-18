using JustbokApplication.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class IntervalType : PropertyChangedNotification
    {
        public int IntervalTypeId { get { return GetValue(() => IntervalTypeId); }set { SetValue(() => IntervalTypeId, value); } }

        public string Name { get { return GetValue(() => Name); } set { SetValue(() => Name, value); } }
    }
}
