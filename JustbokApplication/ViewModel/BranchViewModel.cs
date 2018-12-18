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
    public class BranchViewModel : BaseViewModel
    {
        private string _branchSearch = "";
        private string _citySearch = "";
        private string _stateSearch = "";

        public BranchViewModel()
        {
            SortColumn = "BranchCode";
            Ascending = true;
            ItemCount = 10;
            RefreshItems();
        }

        public string BranchSearch { get => string.IsNullOrEmpty(_branchSearch) ? "" : _branchSearch; set => _branchSearch = value; }
        public string CitySearch { get => string.IsNullOrEmpty(_citySearch) ? "" : _citySearch; set => _citySearch = value; }
        public string StateSearch { get => string.IsNullOrEmpty(_stateSearch) ? "" : _stateSearch; set => _stateSearch = value; }

        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new BranchDao().GetBranches(BranchSearch,CitySearch,StateSearch, SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount);

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

                NotifyPropertyChanged("BranchSearch");
                NotifyPropertyChanged("CitySearch");
                NotifyPropertyChanged("StateSearch");
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
            BranchInformationById(Convert.ToInt32(obj));
            DialogContent = new AddEditBranchView();
            IsDialogOpen = true;
            HideLoader();
        }

        public override void SaveContent(object obj)
        {
            try
            {
                ShowLoader();
                int BranchId = new BranchDao().BranchInsUpd((Branch)Item, SessionManager.UserId);
                IsDialogOpen = false;
                HideLoader();
                BranchSearch = "";
                CitySearch = "";
                StateSearch = "";
                StartIndex = 1;
                RefreshItems();
            }
            catch (Exception)
            {

            }
        }

        private void BranchInformationById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Item = new BranchDao().BranchById(Id);

                    if (Item == null)
                    {
                        Item = new Branch();
                    }
                }
                else
                {
                    Item = new Branch();
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
                int BranchId = new BranchDao().DeleteBranchById((int)Id,SessionManager.UserId);
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
            BranchSearch = "";
            CitySearch = "";
            StateSearch = "";
        }
    }
}
