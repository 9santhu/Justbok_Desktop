using System;
using System.Windows;

namespace JustbokApplication.Models
{
    public class Stock : PropertyChangedNotification
    {
        public int ProductId { get { return GetValue(() => ProductId); } set { SetValue(() => ProductId, value); } }
        public string BrandName { get { return GetValue(() => BrandName); } set { SetValue(() => BrandName, value); } }
        public string ProductName { get { return GetValue(() => ProductName); } set { SetValue(() => ProductName, value); } }
        public DateTime StockDate { get { return GetValue(() => StockDate); } set { SetValue(() => StockDate, value); } }
        public int CurrentStock { get { return GetValue(() => CurrentStock); } set { SetValue(() => CurrentStock, value); } }
        public int AddedQuantity { get { return GetValue(() => AddedQuantity); } set { SetValue(() => AddedQuantity, value); } }
        public Visibility IsVisible { get { return GetValue(() => IsVisible); } set { SetValue(() => IsVisible, value); } }
    }
}
