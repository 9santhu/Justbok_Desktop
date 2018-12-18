using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class Branch
    {
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; }
    }
}
