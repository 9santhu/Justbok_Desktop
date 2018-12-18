using JustbokApplication.Data;
using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.ViewModel
{
    public class MainWindowViewModel
    {
        private IList<Branch> _branches;

        public IList<Branch> Branches
        {
            get { return _branches; }
            set { _branches = value; }
        }

        private static Branch _sbranch;

        public Branch SBranch
        {
            get { return _sbranch; }
            set { _sbranch = value; }
        }

        public MainWindowViewModel()
        {
            _branches = new BranchDao().BranchesByUser(SessionManager.UserId, SessionManager.UserRole);

            if (_branches != null)
            {
                _sbranch = _branches.FirstOrDefault();
            }
        }
    }
}
