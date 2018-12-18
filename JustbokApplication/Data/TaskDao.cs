using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
    public class TaskDao
    {
        public Result GetTasks(string titleSearch, string descriptionSearch,DateTime fromDate,DateTime toDate,
                               string orderBy, string direction,int startRowIndex, int maximumRows,int branchId)
        {
            Result objResult = new Result();
            try
            {
                var param = new DbParam[9];

                param[0] = new DbParam("@SortBy", orderBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SortDirection", direction, SqlDbType.VarChar);
                param[2] = new DbParam("@StartRowIndex", startRowIndex, SqlDbType.Int);
                param[3] = new DbParam("@MaximumRows", maximumRows, SqlDbType.Int);
                param[4] = new DbParam("@TitleSearch", titleSearch, SqlDbType.VarChar);
                param[5] = new DbParam("@DescriptionSearch", descriptionSearch, SqlDbType.VarChar);
                param[6] = new DbParam("@FromDate", fromDate, SqlDbType.DateTime);
                param[7] = new DbParam("@ToDate", toDate, SqlDbType.DateTime);
                param[8] = new DbParam("@BranchId", branchId, SqlDbType.Int);

                DataSet ds = Db.GetDataSet("SP_TASK_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<Models.Task> tasks = new List<Models.Task>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.Task task = new Models.Task();
                        task.TaskId = Db.ToInteger(row["TaskId"]);
                        task.Title = Db.ToString(row["Title"]);
                        task.Description = Db.ToString(row["Description"]);
                        task.Interval = Db.ToInteger(row["Interval"]);
                        task.IntervalType = new IntervalType();
                        task.IntervalType.Name = Db.ToString(row["Name"]);
                        task.StartDate = Db.ToDateTime(row["StartDate"]);
                        tasks.Add(task);
                    }

                    objResult.Items = tasks;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["TaskCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int TaskInsUpd(Models.Task task, int StaffId)
        {
            int SupplierId = 0;
            try
            {
                var param = new DbParam[9];

                param[0] = new DbParam("@TaskId", task.TaskId, SqlDbType.Int);
                param[1] = new DbParam("@Title", task.Title, SqlDbType.VarChar);
                param[2] = new DbParam("@Description", task.Description, SqlDbType.VarChar);
                param[3] = new DbParam("@Interval", task.Interval, SqlDbType.Int);
                param[4] = new DbParam("@IntervalTypeId", task.IntervalType.IntervalTypeId, SqlDbType.Int);
                param[5] = new DbParam("@StartDate", task.StartDate, SqlDbType.DateTime);
                param[6] = new DbParam("@IsActive", task.IsActive, SqlDbType.Bit);
                param[7] = new DbParam("@CreatedBy", StaffId, SqlDbType.Int);
                param[8] = new DbParam("@BranchId", task.BranchId, SqlDbType.Int);

                SupplierId = Db.Insert("SP_TASK_INSUPD", param, true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return SupplierId;
        }

        public Models.Task TaskById(int taskId)
        {
            Models.Task task = null;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@TaskId", taskId, SqlDbType.Int);

                DataRow row = Db.GetDataRow("SP_TASK_BY_ID", param);

                if (row != null)
                {
                    task = new Models.Task();

                    task.TaskId = Db.ToInteger(row["TaskId"]);
                    task.Title = Db.ToString(row["Title"]);
                    task.Description = Db.ToString(row["Description"]);
                    task.Interval = Db.ToInteger(row["Interval"]);
                    task.IntervalType = new IntervalType();
                    task.IntervalType.IntervalTypeId= Db.ToInteger(row["IntervalTypeId"]);
                    task.IntervalType.Name= Db.ToString(row["Name"]);
                    task.StartDate = Db.ToDateTime(row["StartDate"]);
                    task.IsActive = Db.ToBoolean(row["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return task;
        }

        public int DeleteTaskById(int taskId, int staffId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[2];

                param[0] = new DbParam("@TaskId", taskId, SqlDbType.Int);
                param[1] = new DbParam("@StaffId", staffId, SqlDbType.Int);

                result = Db.Insert("SP_TASK_DELETE_BY_ID", param, false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }

        public IList<IntervalType> GetActiveIntervalTypes()
        {
            IList<IntervalType> intervalTypes = null;
            try
            {
                DataTable dt = Db.GetDataTable("SP_INTERVALTYPE_GET", null);

                if (dt != null && dt.Rows.Count>0)
                {
                    intervalTypes = new List<IntervalType>();
                    foreach (DataRow row in dt.Rows)
                    {
                        IntervalType intervalType = new IntervalType();

                        intervalType.IntervalTypeId = Db.ToInteger(row["IntervalTypeId"]);
                        intervalType.Name = Db.ToString(row["Name"]);
                        intervalTypes.Add(intervalType);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return intervalTypes;
        }

    }
}
