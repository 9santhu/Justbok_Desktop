using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
    public class ExpenseDao
    {
        public Result GetExpenses(int ExpenseTypeId, string descriptionSearch, DateTime fromDate, DateTime toDate, string orderBy, string direction,
                                  int startRowIndex, int maximumRows, int branchId)
        {
            Result objResult = new Result();
            try
            {
                var param = new DbParam[9];

                param[0] = new DbParam("@SortBy", orderBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SortDirection", direction, SqlDbType.VarChar);
                param[2] = new DbParam("@StartRowIndex", startRowIndex, SqlDbType.Int);
                param[3] = new DbParam("@MaximumRows", maximumRows, SqlDbType.Int);
                param[4] = new DbParam("@ExpenseTypeId", ExpenseTypeId, SqlDbType.Int);
                param[5] = new DbParam("@DescriptionSearch", descriptionSearch, SqlDbType.VarChar);
                param[6] = new DbParam("@FromDate", fromDate, SqlDbType.DateTime);
                param[7] = new DbParam("@ToDate", toDate, SqlDbType.DateTime);
                param[8] = new DbParam("@BranchId", branchId, SqlDbType.Int);

                DataSet ds = Db.GetDataSet("SP_EXPENSES_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<Expenses> expenses = new List<Expenses>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Expenses expense = new Expenses();

                        expense.ExpenseId = Db.ToInteger(row["ExpenseId"]);
                        expense.ExpenseType = new ExpenseType();
                        expense.ExpenseType.TypeName = Db.ToString(row["TypeName"]);
                        expense.ExpenseAmount = Db.ToString(row["ExpenseAmount"]);
                        expense.ExpenseDate = Db.ToDateTime(row["ExpenseDate"]);
                        expense.ExpenseDescription = Db.ToString(row["ExpenseDescription"]);
                        expenses.Add(expense);
                    }

                    objResult.Items = expenses;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["ExpenseCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int ExpenseInsUpd(Expenses expenses, int StaffId)
        {
            int ExpenseId = 0;
            try
            {
                var param = new DbParam[10];

                param[0] = new DbParam("@ExpenseId", expenses.ExpenseId, SqlDbType.Int);
                param[1] = new DbParam("@ExpenseTypeId", expenses.ExpenseType.ExpenseTypeId, SqlDbType.Int);
                param[2] = new DbParam("@ExpenseDate", expenses.ExpenseDate, SqlDbType.DateTime);
                param[3] = new DbParam("@ExpenseAmount", expenses.ExpenseAmount, SqlDbType.Decimal);
                param[4] = new DbParam("@ExpenseModeId", expenses.ExpenseMode.ExpenseModeId, SqlDbType.Int);
                param[5] = new DbParam("@ReferenceNumber", expenses.ReferenceNumber, SqlDbType.VarChar);
                param[6] = new DbParam("@ExpenseDescription", expenses.ExpenseDescription, SqlDbType.VarChar);
                param[7] = new DbParam("@IsActive", expenses.IsActive, SqlDbType.Bit);
                param[8] = new DbParam("@CreatedBy", StaffId, SqlDbType.Int);
                param[9] = new DbParam("@BranchId", expenses.BranchId, SqlDbType.Int);

                ExpenseId = Db.Insert("SP_EXPENSE_INSUPD", param, true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return ExpenseId;
        }

        public Expenses ExpenseById(int expenseId)
        {
            Expenses expenses = null;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@ExpenseId", expenseId, SqlDbType.Int);

                DataRow row = Db.GetDataRow("SP_EXPENSE_BY_ID", param);

                if (row != null)
                {
                    expenses = new Expenses();


                    expenses.ExpenseId = Db.ToInteger(row["ExpenseId"]);
                    expenses.ExpenseType = new ExpenseType();
                    expenses.ExpenseType.TypeName = Db.ToString(row["TypeName"]);
                    expenses.ExpenseType.ExpenseTypeId = Db.ToInteger(row["ExpenseTypeId"]);
                    expenses.ExpenseAmount = Db.ToString(row["ExpenseAmount"]);
                    expenses.ExpenseDate = Db.ToDateTime(row["ExpenseDate"]);
                    expenses.ExpenseDescription = Db.ToString(row["ExpenseDescription"]);
                    expenses.ReferenceNumber = Db.ToString(row["ReferenceNumber"]);
                    expenses.ExpenseMode = new ExpenseMode();
                    expenses.ExpenseMode.ExpenseMod = Db.ToString(row["ExpenseMode"]);
                    expenses.ExpenseMode.ExpenseModeId = Db.ToInteger(row["ExpenseModeId"]);
                    expenses.IsActive = Db.ToBoolean(row["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return expenses;
        }

        public int DeleteExpenseById(int expenseId, int staffId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[2];

                param[0] = new DbParam("@ExpenseId", expenseId, SqlDbType.Int);
                param[1] = new DbParam("@StaffId", staffId, SqlDbType.Int);

                result = Db.Insert("SP_EXPENSE_DELETE_BY_ID", param, false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }

    }
}
