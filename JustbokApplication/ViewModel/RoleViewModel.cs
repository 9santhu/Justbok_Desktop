using JustbokApplication.Data;
using JustbokApplication.Models;
using JustbokApplication.Views;
using JustbokApplication.Views.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.ViewModel
{
    public class RoleViewModel : BaseViewModel
    {
        private string _roleSearch = "";

        public RoleViewModel()
        {
            SortColumn = "RoleId";
            Ascending = true;
            ItemCount = 10;
            RefreshItems();

        }

        public string RoleSearch { get => string.IsNullOrEmpty(_roleSearch) ? "" : _roleSearch; set => _roleSearch = value; }

        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new RoleDao().GetRoles(RoleSearch, SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount);

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
                NotifyPropertyChanged("RoleSearch");
                HideLoader();
            }
            catch (Exception)
            {
                HideLoader();
            }
        }

        public override void OpeningDialog(object obj)
        {
            ShowLoader();
            RoleInformationById(Convert.ToInt32(obj));
            DialogContent = new AddEditRoleView();
            IsDialogOpen = true;
            HideLoader();
        }

        public override void SaveContent(object obj)
        {
            try
            {
                ShowLoader();
                int RoleId = new RoleDao().RoleInsUpd((Role)Item, SessionManager.UserId);
                IsDialogOpen = false;
                HideLoader();
                RoleSearch = "";
                StartIndex = 1;
                RefreshItems();
            }
            catch (Exception)
            {

            }
        }

        private void RoleInformationById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Item = new RoleDao().RoleById(Id);

                    if (Item == null)
                    {
                        Item = new Role();
                    }
                }
                else
                {
                    Item = new Role();
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
                int RoleId = new RoleDao().DeleteRoleById((int)Id);
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
            throw new NotImplementedException();
        }

        public override void SaveSubContent(object obj)
        {
            throw new NotImplementedException();
        }

        public override void ClearValues()
        {
            RoleSearch = "";
        }
    }
}
