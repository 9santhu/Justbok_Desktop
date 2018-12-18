using JustbokApplication.Data;
using JustbokApplication.Helpers;
using JustbokApplication.Models;
using JustbokApplication.ViewModel.Command;
using JustbokApplication.Views;
using JustbokApplication.Views.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace JustbokApplication.ViewModel
{
    public class StockViewModel : BaseViewModel
    {
        private string _brandSearch = "";
        private string _productSearch = "";

        private Visibility _isVisible = Visibility.Collapsed;
        private string _stockHint = "";
        private Visibility _paginationVisible = Visibility.Visible;
        private Visibility _saveVisibility = Visibility.Collapsed;
        private Visibility _addVisibility = Visibility.Visible;



        private int stockType = 0;

        public ICommand StockCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public StockViewModel()
        {
            SortColumn = "BrandName";
            Ascending = true;
            ItemCount = 10;
            RefreshItems();

            StockCommand = new RelayCommand(ShowStock);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
        }

        public string BrandSearch { get => string.IsNullOrEmpty(_brandSearch) ? "" : _brandSearch; set => _brandSearch = value; }
        public string ProductSearch { get => string.IsNullOrEmpty(_productSearch) ? "" : _productSearch; set => _productSearch = value; }

        public Visibility IsVisible { get => _isVisible; set { _isVisible = value;NotifyPropertyChanged("IsVisible"); } }
        public string StockHint { get => _stockHint; set { _stockHint = value; NotifyPropertyChanged("StockHint"); } }
        public Visibility PaginationVisible { get => _paginationVisible; set {_paginationVisible = value; NotifyPropertyChanged("PaginationVisible"); } }
        public Visibility SaveVisibility { get => _saveVisibility; set { _saveVisibility = value; NotifyPropertyChanged("SaveVisibility"); } }
        public Visibility AddVisibility { get => _addVisibility; set { _addVisibility = value; NotifyPropertyChanged("AddVisibility"); } }


        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new StockDao().GetStock(BrandSearch,ProductSearch,SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount,SessionManager.BranchId);

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

        private void ShowStock(object obj)
        {
            if (obj != null)
            {
                ShowLoader();
                PaginationVisible = Visibility.Collapsed;
                AddVisibility = Visibility.Collapsed;
                SaveVisibility = Visibility.Visible;

                if (Items != null)
                {
                    IList<Stock> stocks = (List<Stock>)Items;
                    stocks.ToList().ForEach(x => x.IsVisible = Visibility.Visible);
                    Items = stocks;
                }

                if (Convert.ToInt32(obj) == 1)
                {
                    StockHint = "Add Stock";
                    stockType = 1;
                }
                else
                {
                    StockHint = "Add Stock";
                    stockType = 0;
                }
                HideLoader();
            }
        }

        private void Cancel(object obj)
        {
            if (obj != null)
            {
                ShowLoader();
                if (Items != null)
                {
                    IList<Stock> stocks = (List<Stock>)Items;
                    stocks.ToList().ForEach(x => x.IsVisible = Visibility.Collapsed);
                    Items = stocks;
                }
                PaginationVisible = Visibility.Visible;
                AddVisibility = Visibility.Visible;
                SaveVisibility = Visibility.Collapsed;
                HideLoader();
            }
        }

        private void Save(object obj)
        {
            try
            {
                if (Items != null)
                {
                    ShowLoader();
                    IList<Stock> stocks = (List<Stock>)Items;

                    var data = stocks.Where(x => x.AddedQuantity > 0).ToList();

                    if (data != null && data.Count > 0)
                    {
                        DataTable dt = new DataTable();

                        dt.Columns.Add("ProductId", typeof(Int32));
                        dt.Columns.Add("Quantity", typeof(Int32));
                        dt.Columns.Add("CreatedBy", typeof(Int32));
                        dt.Columns.Add("StockType", typeof(Int32));

                        foreach (var stock in data)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ProductId"] = stock.ProductId;
                            dr["Quantity"] = stock.AddedQuantity;
                            dr["CreatedBy"] = SessionManager.UserId;
                            dr["StockType"] = stockType;
                            dt.Rows.Add(dr);
                        }

                        int Result = new StockDao().StockIns(dt);
                        HideLoader();
                        RefreshItems();
                        if (Result > 0)
                        {
                            new Toaster().ShowSuccess("Stock Information Updated Successfully");
                            Cancel(obj);
                        }
                        else
                        {
                            new Toaster().ShowSuccess("Error occured while updation stock");
                        }
                    }
                }
            }
            catch (Exception)
            {
                HideLoader();
                new Toaster().ShowSuccess("Error occured while updation stock");
            }
        }

        public override void OpeningDialog(object obj)
        {
            throw new NotImplementedException();
        }

        public override void SaveContent(object obj)
        {
            throw new NotImplementedException();
        }

        public override void Delete(object obj)
        {
            throw new NotImplementedException();
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
