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
    public class StockHistoryViewModel : BaseViewModel
    {
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private int _stockType = 1;

        private string _stockText = "Stock Out List";

        public ICommand StockCommand { get; }

        public StockHistoryViewModel()
        {
            SortColumn = "BrandName";
            Ascending = true;
            ItemCount = 10;
            StockCommand = new RelayCommand(ShowStockData);
            RefreshItems();
        }

        public DateTime FromDate { get => _fromDate; set => _fromDate = value; }
        public DateTime ToDate { get => _toDate; set => _toDate = value; }
        public int StockType { get => _stockType; set => _stockType = value; }
        public string StockText { get => _stockText; set => _stockText = value; }

        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new StockDao().GetStockHistory(FromDate,ToDate,StockType,SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount,SessionManager.BranchId);

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
                NotifyPropertyChanged("FromDate");
                NotifyPropertyChanged("ToDate");

                HideLoader();
            }
            catch (Exception)
            {
                HideLoader();
            }
        }

        private void ShowStockData(object obj)
        {
            if (obj != null)
            {
                ShowLoader();
                StockType = (int)obj ==0 ? 1 : 0;

                if (StockType == 1)
                {
                    StockText = "Stock Out List";
                }
                else
                {
                    StockText = "Stock In List";
                }
                NotifyPropertyChanged("StockType");
                NotifyPropertyChanged("StockText");
                HideLoader();
                RefreshItems();
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
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
        }
    }
}
