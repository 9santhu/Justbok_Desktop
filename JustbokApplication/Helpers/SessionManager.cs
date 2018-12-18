using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JustbokApplication
{
    public static class SessionManager
    {
        public static int UserId { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string UserRole { get; set; }

        public static int BranchId { get { return BaseViewModel.BranchControl != null && BaseViewModel.BranchControl.SelectedItem != null ? (BaseViewModel.BranchControl.SelectedItem as Branch).BranchId : 0; } }
    }
}
