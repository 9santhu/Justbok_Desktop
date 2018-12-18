using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
    public class MealTimeDao
    {
        public Result GetMealTimes(string orderBy, string direction,int startRowIndex, int maximumRows, int branchId)
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

                DataSet ds = Db.GetDataSet("SP_MEALTIME_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<MealTime> mealTimes = new List<MealTime>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        MealTime mealTime = new MealTime();

                        mealTime.MealTimeId = Db.ToInteger(row["MealTimeId"]);
                        mealTime.MTime = Db.ToDateTime(row["MealTime"]);
                        mealTime.Description = Db.ToString(row["Description"]);
                        mealTimes.Add(mealTime);
                    }

                    objResult.Items = mealTimes;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["MealTimeCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int MealTimeInsUpd(MealTime mealTime, int StaffId)
        {
            int mealTimeId = 0;
            try
            {
                var param = new DbParam[6];

                param[0] = new DbParam("@MealTimeId", mealTime.MealTimeId, SqlDbType.Int);
                param[1] = new DbParam("@MealTime", mealTime.MTime, SqlDbType.DateTime);
                param[2] = new DbParam("@Description", mealTime.Description, SqlDbType.VarChar);
                param[3] = new DbParam("@IsActive", mealTime.IsActive, SqlDbType.Bit);
                param[4] = new DbParam("@CreatedBy", StaffId, SqlDbType.Int);
                param[5] = new DbParam("@BranchId", mealTime.BranchId, SqlDbType.Int);

                mealTimeId = Db.Insert("SP_MEALTIME_INSUPD", param, true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return mealTimeId;
        }

        public MealTime MealTimeById(int mealTimeId)
        {
            MealTime mealTime = null;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@MealTimeId", mealTimeId, SqlDbType.Int);

                DataRow row = Db.GetDataRow("SP_MEALTIME_BY_ID", param);

                if (row != null)
                {
                    mealTime = new MealTime();


                    mealTime.MealTimeId = Db.ToInteger(row["MealTimeId"]);
                    mealTime.MTime = Db.ToDateTime(row["MealTime"]);
                    mealTime.Description = Db.ToString(row["Description"]);
                    mealTime.IsActive = Db.ToBoolean(row["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return mealTime;
        }

        public int DeleteMealTimeById(int mealTimeId, int staffId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[2];

                param[0] = new DbParam("@MealTimeId", mealTimeId, SqlDbType.Int);
                param[1] = new DbParam("@StaffId", staffId, SqlDbType.Int);

                result = Db.Insert("SP_MEALTIME_DELETE_BY_ID", param, false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }

    }
}
