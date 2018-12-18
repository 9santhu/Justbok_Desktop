using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
    public class ExpenseTypeDao
    {
        public Result GetExpenseTypes(string nameSearch, string descriptionSearch, string orderBy, string direction,
                                  int startRowIndex, int maximumRows,int branchId)
        {
            Result objResult = new Result();
            try
            {
                var param = new DbParam[7];

                param[0] = new DbParam("@SortBy", orderBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SortDirection", direction, SqlDbType.VarChar);
                param[2] = new DbParam("@StartRowIndex", startRowIndex, SqlDbType.Int);
                param[3] = new DbParam("@MaximumRows", maximumRows, SqlDbType.Int);
                param[4] = new DbParam("@NameSearch", nameSearch, SqlDbType.VarChar);
                param[5] = new DbParam("@DescriptionSearch", descriptionSearch, SqlDbType.VarChar);
                param[6] = new DbParam("@BranchId", branchId, SqlDbType.Int);

                DataSet ds = Db.GetDataSet("SP_EXPENSETYPE_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<ExpenseType> expenseTypes = new List<ExpenseType>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ExpenseType expenseType = new ExpenseType();
                        expenseType.ExpenseTypeId = Db.ToInteger(row["ExpenseTypeId"]);
                        expenseType.TypeName = Db.ToString(row["TypeName"]);
                        expenseType.TypeDescription = Db.ToString(row["TypeDescription"]);
                        expenseType.IsActive = Db.ToBoolean(row["IsActive"]);
                        expenseTypes.Add(expenseType);
                    }

                    objResult.Items = expenseTypes;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["TypeCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int ExpenseTypeInsUpd(ExpenseType expenseType, int StaffId)
        {
            int SupplierId = 0;
            try
            {
                var param = new DbParam[6];

                param[0] = new DbParam("@ExpenseTypeId", expenseType.ExpenseTypeId, SqlDbType.Int);
                param[1] = new DbParam("@TypeName", expenseType.TypeName, SqlDbType.VarChar);
                param[2] = new DbParam("@TypeDescription", expenseType.TypeDescription, SqlDbType.VarChar);
                param[3] = new DbParam("@IsActive", expenseType.IsActive, SqlDbType.Bit);
                param[4] = new DbParam("@CreatedBy", StaffId, SqlDbType.Int);
                param[5] = new DbParam("@BranchId", expenseType.BranchId, SqlDbType.Int);

                SupplierId = Db.Insert("SP_EXPENSETYPE_INSUPD", param, true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return SupplierId;
        }

        public ExpenseType ExpenseTypeById(int expenseTypeId)
        {
            ExpenseType expenseType = null;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@ExpenseTypeId", expenseTypeId, SqlDbType.Int);

                DataRow row = Db.GetDataRow("SP_EXPENSETYPE_BY_ID", param);

                if (row != null)
                {
                    expenseType = new ExpenseType();

                    expenseType.ExpenseTypeId = Db.ToInteger(row["ExpenseTypeId"]);
                    expenseType.TypeName = Db.ToString(row["TypeName"]);
                    expenseType.TypeDescription = Db.ToString(row["TypeDescription"]);
                    expenseType.IsActive = Db.ToBoolean(row["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return expenseType;
        }

        public int DeleteExpenseTypeById(int expenseTypeId, int staffId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[2];

                param[0] = new DbParam("@ExpenseTypeId", expenseTypeId, SqlDbType.Int);
                param[1] = new DbParam("@StaffId", staffId, SqlDbType.Int);

                result = Db.Insert("SP_EXPENSETYPE_DELETE_BY_ID", param, false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }

        public IList<ExpenseType> GetActiveExpenseTypes()
        {
            IList<ExpenseType> expenseTypes = null;
            try
            {
                DataTable dt = Db.GetDataTable("SP_EXPENSETYPE_GET_ACTIVE", null);

                if (dt != null && dt.Rows.Count > 0)
                {
                    expenseTypes = new List<ExpenseType>();
                    foreach (DataRow row in dt.Rows)
                    {
                        ExpenseType expenseType = new ExpenseType();

                        expenseType.ExpenseTypeId = Db.ToInteger(row["ExpenseTypeId"]);
                        expenseType.TypeName = Db.ToString(row["TypeName"]);
                        expenseTypes.Add(expenseType);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return expenseTypes;
        }

    }
}
