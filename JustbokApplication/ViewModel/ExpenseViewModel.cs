using JustbokApplication.Data;
using JustbokApplication.Helpers;
using JustbokApplication.Models;
using JustbokApplication.Views;
using JustbokApplication.Views.Settings;
using System;
using System.Collections.Generic;

namespace JustbokApplication.ViewModel
{
    public class ExpenseViewModel : BaseViewModel
    {
        private string _descriptionSearch = "";
        private ExpenseType _expenseTypeSearch;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private IList<ExpenseType> _expenseTypes;

        public ExpenseViewModel()
        {
            Init();
            SortColumn = "ExpenseAmount";
            Ascending = true;
            ItemCount = 10;
            RefreshItems();
            ExpenseTypes = new ExpenseTypeDao().GetActiveExpenseTypes();
        }

        public string DescriptionSearch { get => _descriptionSearch; set => _descriptionSearch = value; }
        public ExpenseType ExpenseTypeSearch { get => _expenseTypeSearch; set => _expenseTypeSearch = value; }
        public DateTime FromDate { get => _fromDate; set => _fromDate = value; }
        public DateTime ToDate { get => _toDate; set => _toDate = value; }
        public IList<ExpenseType> ExpenseTypes { get => _expenseTypes; set => _expenseTypes = value; }

        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new ExpenseDao().GetExpenses(ExpenseTypeSearch != null ? ExpenseTypeSearch.ExpenseTypeId:0, DescriptionSearch, FromDate,ToDate, SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount, SessionManager.BranchId);

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
                NotifyPropertyChanged("ExpenseTypeSearch");
                NotifyPropertyChanged("DescriptionSearch");
                NotifyPropertyChanged("FromDate");
                NotifyPropertyChanged("ToDate");
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
                ExpenseInformationById(Convert.ToInt32(obj));
                DialogContent = new AddEditExpense();
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
                Expenses expenses = Item as Expenses;
                expenses.BranchId = SessionManager.BranchId;
                int expenseId = new ExpenseDao().ExpenseInsUpd(expenses, SessionManager.UserId);
                if (expenseId > 0)
                {
                    new Toaster().ShowSuccess("Expense Information Saved Successfully.");
                    IsDialogOpen = false;
                    HideLoader();
                    DescriptionSearch = "";
                    ExpenseTypeSearch = null;
                    FromDate = DateTime.Now;
                    ToDate = DateTime.Now;
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

        private void ExpenseInformationById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Item = new ExpenseDao().ExpenseById(Id);
                    ((Expenses)Item).ExpenseModes = new ExpenseModeDao().GetActiveExpenseModes();
                    ((Expenses)Item).ExpenseTypes = new ExpenseTypeDao().GetActiveExpenseTypes();

                    if (Item == null)
                    {
                        Expenses expenses = new Expenses();
                        expenses.ExpenseModes = new ExpenseModeDao().GetActiveExpenseModes();
                        expenses.ExpenseTypes = new ExpenseTypeDao().GetActiveExpenseTypes();
                        expenses.ExpenseType = null;
                        expenses.ExpenseMode = null;
                        Item = expenses;

                    }
                }
                else
                {
                    Expenses expenses = new Expenses();
                    expenses.ExpenseModes = new ExpenseModeDao().GetActiveExpenseModes();
                    expenses.ExpenseTypes = new ExpenseTypeDao().GetActiveExpenseTypes();
                    expenses.ExpenseType = null;
                    expenses.ExpenseMode = null;
                    Item = expenses;
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
                int Result = new ExpenseDao().DeleteExpenseById((int)Id, SessionManager.UserId);

                if (Result > 0)
                {
                    IsDialogOpen = false;
                    HideLoader();
                    StartIndex = 1;
                    RefreshItems();
                    new Toaster().ShowSuccess("Expense Information deleted successfully.");
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
            DescriptionSearch = "";
            ExpenseTypeSearch = null;
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
        }
    }
}
