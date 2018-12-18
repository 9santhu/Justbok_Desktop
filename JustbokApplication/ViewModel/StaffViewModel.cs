using JustbokApplication.Controls;
using JustbokApplication.Data;
using JustbokApplication.Models;
using JustbokApplication.ViewModel.Command;
using JustbokApplication.Views;
using JustbokApplication.Views.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace JustbokApplication.ViewModel
{
    public class StaffViewModel : BaseViewModel
    {
        private string _staffNameSearch = "";
        private string _roleSearch = "";
        private string _mailSearch = "";
        private string _phoneSearch = "";

        private string _branchNames;
        private string _shiftNames;

        private IList<Role> _roles;

        public StaffViewModel()
        {
            SortColumn = "UserName";
            Ascending = true;
            ItemCount = 10;
            RefreshItems();

        }

        public string StaffNameSearch { get => _staffNameSearch; set => _staffNameSearch = value; }
        public string RoleSearch { get => _roleSearch; set => _roleSearch = value; }
        public string MailSearch { get => _mailSearch; set => _mailSearch = value; }
        public string PhoneSearch { get => _phoneSearch; set => _phoneSearch = value; }

        public IList<Role> Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                _roles = value;
                NotifyPropertyChanged("Roles");
            }
        }

        public string BranchNames
        {
            get
            {
                return _branchNames;
            }
            set
            {
                if (object.ReferenceEquals(_branchNames, value) != true)
                {
                    _branchNames = value;
                    NotifyPropertyChanged("BranchNames");
                }
            }
        }

        public string ShiftNames
        {
            get
            {
                return _shiftNames;
            }
            set
            {
                if (object.ReferenceEquals(_shiftNames, value) != true)
                {
                    _shiftNames = value;
                    NotifyPropertyChanged("ShiftNames");
                }
            }
        }

        public override void OpeningDialog(object obj)
        {
            ShowLoader();
            Roles = new RoleDao().GetActiveRoles();
            UserInformationById(Convert.ToInt32(obj));
            SetBranchandShiftData();
            DialogContent = null;
            DialogContent = new AddEditStaffView();
            IsDialogOpen = true;
            HideLoader();
        }

        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new StaffDao().GetStaff(StaffNameSearch, RoleSearch, MailSearch, PhoneSearch, SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount,SessionManager.BranchId);

                if (result != null)
                {
                    Items = result.Items;
                    TotalItems = result.ItemCount;
                }
                else
                {
                    Items = null;
                    TotalItems = 0;
                }
                NotifyPropertyChanged("Start");
                NotifyPropertyChanged("End");
                NotifyPropertyChanged("TotalItems");
                NotifyPropertyChanged("StaffNameSearch");
                NotifyPropertyChanged("RoleSearch");
                NotifyPropertyChanged("MailSearch");
                NotifyPropertyChanged("PhoneSearch");


                HideLoader();
            }
            catch (Exception)
            {
                HideLoader();
            }
        }

        public override void SaveContent(object obj)
        {
            try
            {
                ShowLoader();
                int StaffId = new StaffDao().StaffInsUpd((Staff)Item, SessionManager.UserId);
                if (StaffId > 0)
                {
                    IsDialogOpen = false;
                    HideLoader();
                    RoleSearch = "";
                    StartIndex = 1;
                    RefreshItems();
                }
                else if (StaffId == -100)
                {
                    MessageBox.Show("User Name Already Exists.", "Justbok - Validation", MessageBoxButton.OK);
                }
            }
            catch (Exception)
            {

            }
        }

        private void UserInformationById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Item = new StaffDao().StaffById(Id);


                    if (Item == null)
                    {
                        Item = new Staff();
                    }
                }
                else
                {
                    Item = new Staff();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void Delete(object obj)
        {
            try
            {
                ShowLoader();
                int RoleId = new StaffDao().DeleteUserById((int)Id);
                IsDialogOpen = false;
                HideLoader();
                StartIndex = 1;
                RefreshItems();
            }
            catch (Exception)
            {

            }
        }

        public override void OpeningSubDialog(object obj)
        {
            ShowLoader();
            if (obj.ToString().ToLower() == "branch")
            {
                DataItems = new BranchDao().BranchesByUser(SessionManager.UserRole, SessionManager.UserId);

                if (Item != null && DataItems != null && ((Staff)Item).SelectedBranches.Count > 0)
                {
                    List<Node> SelectedValues = ((Staff)Item).SelectedBranches;

                    DataItems = (from dataItems in DataItems
                                 join selectedValues in SelectedValues on dataItems.Id equals selectedValues.Id into joinedList
                                 from sub in joinedList.DefaultIfEmpty()
                                 select new Node
                                 {
                                     Id = dataItems.Id,
                                     Name = dataItems.Name,
                                     IsSelected = sub == null ? false : true
                                 }).ToList();
                }
            }
            else if (obj.ToString().ToLower() == "shift")
            {
                DataItems = new ShiftDao().GetActiveShifts();

                if (Item != null && DataItems != null && ((Staff)Item).SelectedShifts.Count > 0)
                {
                    List<Node> SelectedValues = ((Staff)Item).SelectedShifts;

                    DataItems = (from dataItems in DataItems
                                 join selectedValues in SelectedValues on dataItems.Id equals selectedValues.Id into joinedList
                                 from sub in joinedList.DefaultIfEmpty()
                                 select new Node
                                 {
                                     Id = dataItems.Id,
                                     Name = dataItems.Name,
                                     IsSelected = sub == null ? false : true
                                 }).ToList();
                }
            }
            SubDialogCommandParameter = obj.ToString();
            SubDialogContent = null;
            SubDialogContent = new MultiSelectControl();
            IsSubDialogOpen = true;
            HideLoader();
        }

        public override void SaveSubContent(object obj)
        {
            ShowLoader();
            if (obj.ToString().ToLower() == "branch")
            {
                Staff staff = Item as Staff;
                staff.SelectedBranches = DataItems.Where(x => x.IsSelected == true).ToList();
                SetBranchandShiftData();
            }
            else
            {
                Staff staff = Item as Staff;
                staff.SelectedShifts = DataItems.Where(x => x.IsSelected == true).ToList();
                SetBranchandShiftData();
            }

            IsSubDialogOpen = false;
            HideLoader();
        }

        private void SetBranchandShiftData()
        {
            try
            {
                Staff staff = Item as Staff;

                if (staff.SelectedBranches != null)
                {
                    BranchNames = "";

                    foreach (var branch in staff.SelectedBranches)
                    {
                        if (BranchNames != "")
                        {
                            BranchNames = BranchNames + "," + branch.Name.ToString();
                        }
                        else
                        {
                            BranchNames = branch.Name.ToString();
                        }
                    }
                }

                if (staff.SelectedShifts != null)
                {
                    ShiftNames = "";

                    foreach (var shift in staff.SelectedShifts)
                    {
                        if (ShiftNames != "")
                        {
                            ShiftNames = ShiftNames + "," + shift.Name.ToString();
                        }
                        else
                        {
                            ShiftNames = shift.Name.ToString();
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public override void ClearValues()
        {
            StaffNameSearch = "";
            RoleSearch = "";
            MailSearch = "";
            PhoneSearch = "";
        }
    }
}
