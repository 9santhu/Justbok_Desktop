using JustbokApplication.Data;
using JustbokApplication.Helpers;
using JustbokApplication.Models;
using JustbokApplication.Views;
using JustbokApplication.Views.Settings;
using System;
using System.Collections.Generic;

namespace JustbokApplication.ViewModel
{
    public class PackageViewModel : BaseViewModel
    {
        private string _nameSearch = "";
        private int _monthSearch = 0;
        private float _amountSearch = 0;

        public PackageViewModel()
        {
            SortColumn = "Name";
            Ascending = true;
            ItemCount = 10;
            RefreshItems();
        }

        public string NameSearch { get => _nameSearch; set => _nameSearch = value; }
        public int MonthSearch { get => _monthSearch; set => _monthSearch = value; }
        public float AmountSearch { get => _amountSearch; set => _amountSearch = value; }

        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new PackageDao().GetPackages(NameSearch, MonthSearch, AmountSearch, SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount, SessionManager.BranchId);

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
                NotifyPropertyChanged("NameSearch");
                NotifyPropertyChanged("MonthSearch");
                NotifyPropertyChanged("AmountSearch");
                HideLoader();
            }
            catch (Exception)
            {
                HideLoader();
            }
        }

        public override void OpeningDialog(object obj)
        {
            try
            {
                ShowLoader();
                PackageInformationById(Convert.ToInt32(obj));
                DialogContent = new AddEditPackageView();
                IsDialogOpen = true;
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
                Package package = Item as Package;
                package.BranchId = SessionManager.BranchId;
                int PackageId = new PackageDao().PackageInsUpd(package, SessionManager.UserId);
                if (PackageId > 0)
                {
                    new Toaster().ShowSuccess("Package Information Saved Successfully.");
                    IsDialogOpen = false;
                    HideLoader();
                    NameSearch = "";
                    MonthSearch = 0;
                    AmountSearch = 0;
                    StartIndex = 1;
                    RefreshItems();
                }
                else
                {
                    HideLoader();
                    new Toaster().ShowError("Error Occured While Saving.");
                }
            }
            catch (Exception)
            {
                HideLoader();
                new Toaster().ShowError("Error Occured While Saving.");
            }
        }

        private void PackageInformationById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Item = new PackageDao().PackageById(Id);
                    ((Package)Item).Categories = new CategoryDao().GetAllActiveCategories();

                    if (Item == null)
                    {
                        Item = new Package();
                        ((Package)Item).Categories = new CategoryDao().GetAllActiveCategories();
                    }
                }
                else
                {
                    Item = new Package();
                    ((Package)Item).Categories = new CategoryDao().GetAllActiveCategories();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Delete(object obj)
        {
            try
            {
                ShowLoader();
                int Result = new PackageDao().DeletePackageById((int)Id, SessionManager.UserId);

                if (Result > 0)
                {
                    IsDialogOpen = false;
                    HideLoader();
                    StartIndex = 1;
                    RefreshItems();
                    new Toaster().ShowSuccess("Package Information deleted successfully.");
                }
                else
                {
                    new Toaster().ShowSuccess("Error occured while deleting.");
                }
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
            NameSearch = "";
            MonthSearch = 0;
            AmountSearch = 0;
        }
    }
}
