using JustbokApplication.Data;
using JustbokApplication.Models;
using JustbokApplication.Views;
using JustbokApplication.Views.Settings;
using System;

namespace JustbokApplication.ViewModel
{
    public class ProductViewModel : BaseViewModel
    {
        private string _brandSearch = "";
        private string _productSearch = "";

        public ProductViewModel()
        {
            SortColumn = "BrandName";
            Ascending = true;
            ItemCount = 10;
            RefreshItems();
        }

        public string BrandSearch { get => string.IsNullOrEmpty(_brandSearch) ? "" : _brandSearch; set => _brandSearch = value; }
        public string ProductSearch { get => string.IsNullOrEmpty(_productSearch) ? "" : _productSearch; set => _productSearch = value; }

        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new ProductDao().GetProducts(BrandSearch,ProductSearch,SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount,SessionManager.BranchId);

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
                NotifyPropertyChanged("BrandSearch");
                NotifyPropertyChanged("ProductSearch");

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
            ProductInformationById(Convert.ToInt32(obj));
            DialogContent = new AddEditProductView();
            IsDialogOpen = true;
            HideLoader();
        }

        public override void SaveContent(object obj)
        {
            try
            {
                ShowLoader();
                Product product = Item as Product;
                product.BranchId = SessionManager.BranchId;
                int ProducId = new ProductDao().ProductInsUpd(product, SessionManager.UserId);
                IsDialogOpen = false;
                HideLoader();
                BrandSearch = "";
                ProductSearch = "";
                StartIndex = 1;
                RefreshItems();
            }
            catch (Exception)
            {

            }
        }

        private void ProductInformationById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Item = new ProductDao().ProductById(Id);

                    if (Item == null)
                    {
                        Item = new Product();
                    }
                }
                else
                {
                    Item = new Product();
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
                int ProductId = new ProductDao().DeleteProductById((int)Id,SessionManager.UserId);
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
            BrandSearch = "";
            ProductSearch = "";
        }
    }
}
