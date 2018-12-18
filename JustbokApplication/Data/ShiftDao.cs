using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
     public class ShiftDao
    {
        public IList<Node> GetActiveShifts()
        {
            IList<Node> shifts = null;

            DataTable dt = Db.GetDataTable("SP_SHIFTS_BY_USER", null);

            if (dt != null && dt.Rows.Count > 0)
            {
                shifts = new List<Node>();
                foreach (DataRow dr in dt.Rows)
                {
                    shifts.Add(new Node() { Id= Db.ToInteger(dr["ShiftId"]),Name= Db.ToString(dr["Name"]) });
                }
            }

            return shifts;
        }

    }
}
