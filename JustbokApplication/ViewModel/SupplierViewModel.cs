using JustbokApplication.Data;
using JustbokApplication.Models;
using JustbokApplication.Views;
using JustbokApplication.Views.Settings;
using System;

namespace JustbokApplication.ViewModel
{
    public class SupplierViewModel : BaseViewModel
    {
        private string _companySearch = "";
        private string _supplierSearch = "";

        public SupplierViewModel()
        {
            SortColumn = "CompanyName";
            Ascending = true;
            ItemCount = 10;
            RefreshItems();
        }

        public string CompanySearch { get => _companySearch; set => _companySearch = value; }
        public string SupplierSearch { get => _supplierSearch; set => _supplierSearch = value; }

        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new SupplierDao().GetSuppliers(CompanySearch,SupplierSearch,SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount,SessionManager.BranchId);

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
                NotifyPropertyChanged("CompanySearch");
                NotifyPropertyChanged("SupplierSearch");

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
            SupplierInformationById(Convert.ToInt32(obj));
            DialogContent = new AddEditSupplierView();
            IsDialogOpen = true;
            HideLoader();
        }

        public override void SaveContent(object obj)
        {
            try
            {
                ShowLoader();
                Supplier supplier = Item as Supplier;
                supplier.BranchId = SessionManager.BranchId;
                int ProducId = new SupplierDao().SupplierInsUpd(supplier, SessionManager.UserId);
                IsDialogOpen = false;
                HideLoader();
                CompanySearch = "";
                SupplierSearch = "";
                StartIndex = 1;
                RefreshItems();
            }
            catch (Exception)
            {

            }
        }

        private void SupplierInformationById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Item = new SupplierDao().SupplierById(Id);

                    if (Item == null)
                    {
                        Item = new Supplier();
                    }
                }
                else
                {
                    Item = new Supplier();
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
                int ProductId = new SupplierDao().DeleteSupplierById((int)Id,SessionManager.UserId);
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
            CompanySearch = "";
            SupplierSearch = "";
        }
    }
}
