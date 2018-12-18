using JustbokApplication.Data;
using JustbokApplication.Helpers;
using JustbokApplication.Models;
using JustbokApplication.Views;
using JustbokApplication.Views.DietConfig;
using System;

namespace JustbokApplication.ViewModel
{
    public class MealTimeViewModel : BaseViewModel
    {
        public MealTimeViewModel()
        {
            SortColumn = "MealTime";
            Ascending = true;
            ItemCount = 10;
            RefreshItems();
        }

        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new MealTimeDao().GetMealTimes(SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount,SessionManager.BranchId);

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
            MealTimeInformationById(Convert.ToInt32(obj));
            DialogContent = new AddEditMealTimeView();
            IsDialogOpen = true;
            HideLoader();
        }

        public override void SaveContent(object obj)
        {
            try
            {
                ShowLoader();
                MealTime mealTime = Item as MealTime;
                mealTime.BranchId = SessionManager.BranchId;
                int MealTimeId = new MealTimeDao().MealTimeInsUpd(mealTime, SessionManager.UserId);

                if (MealTimeId > 0)
                {
                    IsDialogOpen = false;
                    HideLoader();
                    StartIndex = 1;
                    RefreshItems();
                    new Toaster().ShowSuccess("Meal Time Saved successfully.");
                }
                else
                {
                    HideLoader();
                    new Toaster().ShowError("Error occured while saving.");
                }
            }
            catch (Exception)
            {
                HideLoader();
                new Toaster().ShowError("Error occured while saving.");
            }
        }

        private void MealTimeInformationById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Item = new MealTimeDao().MealTimeById(Id);

                    if (Item == null)
                    {
                        Item = new MealTime();
                    }
                }
                else
                {
                    Item = new MealTime();
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
                int result = new MealTimeDao().DeleteMealTimeById((int)Id,SessionManager.UserId);

                if (result > 0)
                {
                    IsDialogOpen = false;
                    HideLoader();
                    StartIndex = 1;
                    RefreshItems();
                    new Toaster().ShowSuccess("Meal Time deleted successfully.");
                }
                else
                {
                    HideLoader();
                    new Toaster().ShowError("Error occured while deleting.");
                }
            }
            catch (Exception)
            {
                HideLoader();
                new Toaster().ShowError("Error occured while deleting.");
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
            throw new NotImplementedException();
        }
    }
}
