using JustbokApplication.Data;
using JustbokApplication.Helpers;
using JustbokApplication.Models;
using JustbokApplication.Views;
using JustbokApplication.Views.Settings;
using System;
using System.Collections.Generic;

namespace JustbokApplication.ViewModel
{
    public class TaskViewModel : BaseViewModel
    {
        private string _titleSearch = "";
        private string _descriptionSearch = "";
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;

        public TaskViewModel()
        {
            SortColumn = "Title";
            Ascending = true;
            ItemCount = 10;
            RefreshItems();
        }

        public string DescriptionSearch { get => _descriptionSearch; set => _descriptionSearch = value; }
        public string TitleSearch { get => _titleSearch; set => _titleSearch = value; }
        public DateTime FromDate { get => _fromDate; set => _fromDate = value; }
        public DateTime ToDate { get => _toDate; set => _toDate = value; }

        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new TaskDao().GetTasks(TitleSearch, DescriptionSearch, FromDate, ToDate, SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount, SessionManager.BranchId);

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
                NotifyPropertyChanged("TitleSearch");
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
                TaskInformationById(Convert.ToInt32(obj));
                DialogContent = new AddEditTaskView();
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
                Models.Task task = Item as Models.Task;
                task.BranchId = SessionManager.BranchId;
                int TaskId = new TaskDao().TaskInsUpd(task, SessionManager.UserId);
                if (TaskId > 0)
                {
                    new Toaster().ShowSuccess("Task Information Saved Successfully.");
                    IsDialogOpen = false;
                    HideLoader();
                    DescriptionSearch = "";
                    TitleSearch = "";
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

        private void TaskInformationById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Item = new TaskDao().TaskById(Id);
                    ((Models.Task)Item).IntervalTypes = new TaskDao().GetActiveIntervalTypes();

                    if (Item == null)
                    {
                        Item = new Models.Task();
                        ((Models.Task)Item).StartDate = DateTime.Now;
                        ((Models.Task)Item).IntervalTypes = new TaskDao().GetActiveIntervalTypes();
                    }
                }
                else
                {
                    Item = new Models.Task();
                    ((Models.Task)Item).IntervalTypes = new TaskDao().GetActiveIntervalTypes();
                    ((Models.Task)Item).StartDate = DateTime.Now;
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
                int Result = new TaskDao().DeleteTaskById((int)Id, SessionManager.UserId);

                if (Result > 0)
                {
                    IsDialogOpen = false;
                    HideLoader();
                    StartIndex = 1;
                    RefreshItems();
                    new Toaster().ShowSuccess("Task Information deleted successfully.");
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
            TitleSearch = "";
            DescriptionSearch = "";
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
        }
    }
}
