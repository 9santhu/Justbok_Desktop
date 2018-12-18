using JustbokApplication.Data;
using JustbokApplication.Helpers;
using JustbokApplication.Models;
using JustbokApplication.Views;
using JustbokApplication.Views.DietConfig;
using System;
using System.Data;

namespace JustbokApplication.ViewModel
{
    public class DietPlanViewModel : BaseViewModel
    {
        public DietPlanViewModel()
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
                Result result = new DietPlanDao().GetDietPlans(SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount, SessionManager.BranchId);

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
            DietPlanInformationById(Convert.ToInt32(obj));
            DialogContent = new AddEditDietPlanView();
            IsDialogOpen = true;
            HideLoader();
        }

        public override void SaveContent(object obj)
        {
            try
            {
                ShowLoader();
                DietPlan dietPlan = Item as DietPlan;
                dietPlan.BranchId = SessionManager.BranchId;

                DataTable dtPlanDetails = new DataTable();

                dtPlanDetails.Columns.Add(new DataColumn("MealTimeId", typeof(int)));
                dtPlanDetails.Columns.Add(new DataColumn("D_Mon", typeof(string)));
                dtPlanDetails.Columns.Add(new DataColumn("D_Tue", typeof(string)));
                dtPlanDetails.Columns.Add(new DataColumn("D_Wed", typeof(string)));
                dtPlanDetails.Columns.Add(new DataColumn("D_Thu", typeof(string)));
                dtPlanDetails.Columns.Add(new DataColumn("D_Fri", typeof(string)));
                dtPlanDetails.Columns.Add(new DataColumn("D_Sat", typeof(string)));
                dtPlanDetails.Columns.Add(new DataColumn("D_Sun", typeof(string)));

                if (dietPlan.DietPlanDetails != null && dietPlan.DietPlanDetails.Count > 0)
                {
                    foreach (DietPlanDetails plandetails in dietPlan.DietPlanDetails)
                    {
                        DataRow dr = dtPlanDetails.NewRow();

                        dr["MealTimeId"] = plandetails.MealTime.MealTimeId;
                        dr["D_Mon"] = plandetails.D_Mon;
                        dr["D_Tue"] = plandetails.D_Tue;
                        dr["D_Wed"] = plandetails.D_Wed;
                        dr["D_Thu"] = plandetails.D_Thu;
                        dr["D_Fri"] = plandetails.D_Fri;
                        dr["D_Sat"] = plandetails.D_Sat;
                        dr["D_Sun"] = plandetails.D_Sun;

                        dtPlanDetails.Rows.Add(dr);
                    }
                }
                int MealTimeId = new DietPlanDao().DietPlanInsUpd(dietPlan,dtPlanDetails, SessionManager.UserId);

                if (MealTimeId > 0)
                {
                    IsDialogOpen = false;
                    HideLoader();
                    StartIndex = 1;
                    RefreshItems();
                    new Toaster().ShowSuccess("Diet Plan Saved successfully.");
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

        private void DietPlanInformationById(int Id)
        {
            try
            {
                Item = new DietPlanDao().DietPlanById(Id);

                if (Item == null)
                {
                    Item = new DietPlan();
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
                int result = new DietPlanDao().DeleteDietPlanById((int)Id, SessionManager.UserId);

                if (result > 0)
                {
                    IsDialogOpen = false;
                    HideLoader();
                    StartIndex = 1;
                    RefreshItems();
                    new Toaster().ShowSuccess("Diet plan deleted successfully.");
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
