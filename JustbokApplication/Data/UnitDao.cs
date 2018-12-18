using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
     public class UnitDao
    {
        public IList<Unit> GetAllActiveUnits()
        {
            IList<Unit> units = null;

            DataTable dt = Db.GetDataTable("SP_UNITS_GET_ACTIVE", null);

            if (dt != null && dt.Rows.Count > 0)
            {
                units = new List<Unit>();
                foreach (DataRow dr in dt.Rows)
                {
                    Unit unit = new Unit();

                    unit.UnitId = Db.ToInteger(dr["UnitId"]);
                    unit.Description = Db.ToString(dr["Description"]);

                    units.Add(unit);
                }
            }

            return units;
        }

        public IList<Node> BranchesByUser(string UserRole, int UserId)
        {
            IList<Node> branches = null;

            var param = new DbParam[2];

            param[0] = new DbParam("@UserId", UserId, SqlDbType.Int);
            param[1] = new DbParam("@UserRole", UserRole, SqlDbType.VarChar);

            DataTable dt = Db.GetDataTable("SP_BRANCES_BY_USER", param);

            if (dt != null && dt.Rows.Count > 0)
            {
                branches = new List<Node>();
                foreach (DataRow dr in dt.Rows)
                {
                    branches.Add(new Node() { Id=Db.ToInteger(dr["BranchId"]), Name = Db.ToString(dr["BranchName"])});
                }
            }

            return branches;
        }

    }
}
