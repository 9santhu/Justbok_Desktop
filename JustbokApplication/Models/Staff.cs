using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JustbokApplication.Models
{
    public class Staff
    {
        public Staff()
        {
            //this.Role = new Role();
            this.DOB = DateTime.Now.Date;
            this.SelectedShifts = new List<Node>();
            this.SelectedBranches = new List<Node>();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get { return FirstName + " " + LastName; } }
        public DateTime DOB { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal DailySalary { get; set; }
        public Role Role { get; set; }
        public bool IsActive { get; set; }

        public Visibility IsEdit { get; set; }
        public Visibility IsDelete { get; set; }

        public List<Node> SelectedBranches { get; set; }

        public List<Node> SelectedShifts { get; set; }

        public string strBranches
        {
            get
            {
                string strbranch = "";
                foreach (var branch in SelectedBranches)
                {
                    if (strbranch != "")
                    {

                        strbranch = strbranch + "^" + branch.Id.ToString();
                    }
                    else
                    {
                        strbranch = branch.Id.ToString();
                    }
                }
                return strbranch;
            }
        }

        public string strShifts
        {
            get
            {
                string strShift = "";
                foreach (var shift in SelectedShifts)
                {
                    if (strShift != "")
                    {

                        strShift = strShift + "^" + shift.Id.ToString();
                    }
                    else
                    {
                        strShift = shift.Id.ToString();
                    }
                }
                return strShift;
            }
        }
    }
}

