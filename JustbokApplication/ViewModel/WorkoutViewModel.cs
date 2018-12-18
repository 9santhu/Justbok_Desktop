using JustbokApplication.Data;
using JustbokApplication.Helpers;
using JustbokApplication.Models;
using JustbokApplication.Views.WorkoutConfig;
using System;
using System.Collections.Generic;

namespace JustbokApplication.ViewModel
{
    public class WorkoutViewModel : BaseViewModel
    {
        private string _description = "";
        private Unit _unit = null;
        private IList<Unit> _units = null;

        public string Description { get => _description; set => _description = value; }
        public Unit Unit { get => _unit; set => _unit = value; }
        public IList<Unit> Units { get => _units; set => _units = value; }

        public WorkoutViewModel()
        {
            SortColumn = "Description";
            Ascending = true;
            ItemCount = 10;
            Units = new UnitDao().GetAllActiveUnits();
            RefreshItems();
        }

        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new WorkoutDao().GetWorkouts(Description, Unit!=null ? Unit.UnitId : 0,SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount,SessionManager.BranchId);

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
                NotifyPropertyChanged("Units");
                NotifyPropertyChanged("Unit");
                NotifyPropertyChanged("Description");
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
            WorkoutInformationById(Convert.ToInt32(obj));
            DialogContent = new AddEditWorkoutView();
            IsDialogOpen = true;
            HideLoader();
        }

        public override void SaveContent(object obj)
        {
            try
            {
                ShowLoader();
                Workout workout = Item as Workout;
                workout.BranchId = SessionManager.BranchId;
                int WorkoutId = new WorkoutDao().WorkoutInsUpd(workout, SessionManager.UserId);

                if (WorkoutId > 0)
                {
                    IsDialogOpen = false;
                    HideLoader();
                    StartIndex = 1;
                    RefreshItems();
                    new Toaster().ShowSuccess("Workout Saved successfully.");
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

        private void WorkoutInformationById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Item = new WorkoutDao().WorkoutById(Id);

                    if (Item == null)
                    {
                        Item = new Workout();
                    }
                }
                else
                {
                    Item = new Workout();
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
                int result = new WorkoutDao().DeleteWorkoutById((int)Id,SessionManager.UserId);

                if (result > 0)
                {
                    IsDialogOpen = false;
                    HideLoader();
                    StartIndex = 1;
                    RefreshItems();
                    new Toaster().ShowSuccess("workout deleted successfully.");
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
            Description = "";
            Unit = null;
        }
    }
}
