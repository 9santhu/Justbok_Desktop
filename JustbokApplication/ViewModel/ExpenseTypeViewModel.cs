using JustbokApplication.Data;
using JustbokApplication.Helpers;
using JustbokApplication.Models;
using JustbokApplication.Views;
using JustbokApplication.Views.Settings;
using System;

namespace JustbokApplication.ViewModel
{
    public class ExpenseTypeViewModel : BaseViewModel
    {
        private string _nameSearch = "";
        private string _descriptionSearch = "";

        public ExpenseTypeViewModel()
        {
            SortColumn = "TypeName";
            Ascending = true;
            ItemCount = 10;
            RefreshItems();
        }

        public string DescriptionSearch { get => _descriptionSearch; set => _descriptionSearch = value; }
        public string NameSearch { get => _nameSearch; set => _nameSearch = value; }

        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new ExpenseTypeDao().GetExpenseTypes(NameSearch,DescriptionSearch,SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount,SessionManager.BranchId);

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
                NotifyPropertyChanged("DescriptionSearch");
                NotifyPropertyChanged("NameSearch");

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
            ExpenseTypeInformationById(Convert.ToInt32(obj));
            DialogContent = new AddEditExpenseTypeView();
            IsDialogOpen = true;
            HideLoader();
        }

        public override void SaveContent(object obj)
        {
            try
            {
                ShowLoader();
                ExpenseType expenseType = Item as ExpenseType;
                expenseType.BranchId = SessionManager.BranchId;
                int ProducId = new ExpenseTypeDao().ExpenseTypeInsUpd(expenseType, SessionManager.UserId);
                if (ProducId > 0)
                {
                    new Toaster().ShowSuccess("Expense Type Information Saved Successfully.");
                    IsDialogOpen = false;
                    HideLoader();
                    DescriptionSearch = "";
                    NameSearch = "";
                    StartIndex = 1;
                    RefreshItems();
                }
                else
                {
                    new Toaster().ShowError("Error Occured While Saving.");
                }
            }
            catch (Exception)
            {

            }
        }

        private void ExpenseTypeInformationById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Item = new ExpenseTypeDao().ExpenseTypeById(Id);

                    if (Item == null)
                    {
                        Item = new ExpenseType();
                    }
                }
                else
                {
                    Item = new ExpenseType();
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
                int ProductId = new ExpenseTypeDao().DeleteExpenseTypeById((int)Id,SessionManager.UserId);
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
            NameSearch = "";
            DescriptionSearch = "";
        }
    }
}
