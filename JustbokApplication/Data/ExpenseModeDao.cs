using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
    public class ExpenseModeDao
    {
        public IList<ExpenseMode> GetActiveExpenseModes()
        {
            IList<ExpenseMode> expenseModes = null;
            try
            {
                DataTable dt = Db.GetDataTable("SP_EXPENSEMODE_GET_ACTIVE", null);

                if (dt != null && dt.Rows.Count > 0)
                {
                    expenseModes = new List<ExpenseMode>();
                    foreach (DataRow row in dt.Rows)
                    {
                        ExpenseMode expenseMode = new ExpenseMode();

                        expenseMode.ExpenseModeId = Db.ToInteger(row["ExpenseModeId"]);
                        expenseMode.ExpenseMod = Db.ToString(row["ExpenseMode"]);
                        expenseModes.Add(expenseMode);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return expenseModes;
        }
    }
}
