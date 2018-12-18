using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
    public class DietPlanDao
    {
        public Result GetDietPlans(string orderBy, string direction,int startRowIndex, int maximumRows, int branchId)
        {
            Result objResult = new Result();
            try
            {
                var param = new DbParam[5];

                param[0] = new DbParam("@SortBy", orderBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SortDirection", direction, SqlDbType.VarChar);
                param[2] = new DbParam("@StartRowIndex", startRowIndex, SqlDbType.Int);
                param[3] = new DbParam("@MaximumRows", maximumRows, SqlDbType.Int);
                param[4] = new DbParam("@BranchId", branchId, SqlDbType.Int);

                DataSet ds = Db.GetDataSet("SP_DIETPLAN_GET", param);

                if (ds != null && ds.Tables.Count > 1 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<DietPlan> dietPlans = new List<DietPlan>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        DietPlan dietPlan = new DietPlan();

                        dietPlan.DietPlanId = Db.ToInteger(row["DietPlanId"]);
                        dietPlan.PlanName = Db.ToString(row["PlanName"]);
                        dietPlans.Add(dietPlan);
                    }

                    objResult.Items = dietPlans;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["DietPlanCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int DietPlanInsUpd(DietPlan dietPlan,DataTable planDetails,int StaffId)
        {
            int dietPlanId = 0;
            try
            {
                var param = new DbParam[6];

                param[0] = new DbParam("@DietPlanId", dietPlan.DietPlanId, SqlDbType.Int);
                param[1] = new DbParam("@PlanName", dietPlan.PlanName, SqlDbType.VarChar);
                param[2] = new DbParam("@PlanDetail", planDetails, SqlDbType.Structured);
                param[3] = new DbParam("@IsActive", dietPlan.IsActive, SqlDbType.Bit);
                param[4] = new DbParam("@CreatedBy", StaffId, SqlDbType.Int);
                param[5] = new DbParam("@BranchId", dietPlan.BranchId, SqlDbType.Int);

                dietPlanId = Db.Insert("SP_DIETPLAN_INSUPD", param, true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return dietPlanId;
        }

        public DietPlan DietPlanById(int dietPlanId)
        {
            DietPlan dietPlan = null;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@DietPlanId", dietPlanId, SqlDbType.Int);

                DataSet ds = Db.GetDataSet("SP_DIETPLAN_BY_ID", param);

                if (ds != null && ds.Tables.Count>1)
                {
                    dietPlan = new DietPlan();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            dietPlan = new DietPlan();
                            dietPlan.DietPlanId = Db.ToInteger(row["DietPlanId"]);
                            dietPlan.PlanName = Db.ToString(row["PlanName"]);
                            dietPlan.IsActive = Db.ToBoolean(row["IsActive"]);
                        }
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        IList<DietPlanDetails> dietPlanDetails = new List<DietPlanDetails>();

                        foreach (DataRow row in ds.Tables[1].Rows)
                        {
                            DietPlanDetails dietPlanDetail = new DietPlanDetails();

                            dietPlanDetail.MealTime.MTime = Db.ToDateTime(row["MealTime"]);
                            dietPlanDetail.MealTime.MealTimeId = Db.ToInteger(row["MealTimeId"]);

                            dietPlanDetail.D_Mon = Db.ToString(row["D_Mon"]);
                            dietPlanDetail.D_Tue = Db.ToString(row["D_Tue"]);
                            dietPlanDetail.D_Wed = Db.ToString(row["D_Wed"]);
                            dietPlanDetail.D_Thu = Db.ToString(row["D_Thu"]);
                            dietPlanDetail.D_Fri = Db.ToString(row["D_Fri"]);
                            dietPlanDetail.D_Sat = Db.ToString(row["D_Sat"]);
                            dietPlanDetail.D_Sun = Db.ToString(row["D_Sun"]);

                            dietPlanDetails.Add(dietPlanDetail);
                        }

                        dietPlan.DietPlanDetails = dietPlanDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return dietPlan;
        }

        public int DeleteDietPlanById(int dietPlanId, int staffId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[2];

                param[0] = new DbParam("@DietPlanId", dietPlanId, SqlDbType.Int);
                param[1] = new DbParam("@StaffId", staffId, SqlDbType.Int);

                result = Db.Insert("SP_DIETPLAN_DELETE_BY_ID", param, false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }

    }
}
