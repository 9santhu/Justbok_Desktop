using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
     public class BranchDao
    {
        public Result GetBranches(string branchSearch,string citySearch,string stateSearch,string orderBy,string direction, 
                                  int startRowIndex, int maximumRows)
        {
            Result objResult = new Result();
            try
            {
                var param = new DbParam[7];

                param[0] = new DbParam("@SortBy", orderBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SortDirection", direction, SqlDbType.VarChar);
                param[2] = new DbParam("@StartRowIndex", startRowIndex, SqlDbType.Int);
                param[3] = new DbParam("@MaximumRows", maximumRows, SqlDbType.Int);
                param[4] = new DbParam("@BranchSearch", branchSearch, SqlDbType.VarChar);
                param[5] = new DbParam("@CitySearch", citySearch, SqlDbType.VarChar);
                param[6] = new DbParam("@StateSearch", stateSearch, SqlDbType.VarChar);

                DataSet ds = Db.GetDataSet("SP_BRANCH_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<Branch> branches = new List<Branch>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Branch branch = new Branch();
                        branch.BranchId = Db.ToInteger(row["BranchId"]);
                        branch.BranchCode = Db.ToString(row["BranchCode"]);
                        branch.BranchName = Db.ToString(row["BranchName"]);
                        branch.Address = Db.ToString(row["Address"]);
                        branch.PhoneNo = Db.ToString(row["PhoneNo"]);
                        branch.City = Db.ToString(row["City"]);
                        branch.State = Db.ToString(row["State"]);
                        branches.Add(branch);
                    }

                    objResult.Items = branches;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["BranchCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int BranchInsUpd(Branch branch,int StaffId)
        {
            int RoleId = 0;
            try
            {
                var param = new DbParam[9];

                param[0] = new DbParam("@BranchId", branch.BranchId, SqlDbType.Int);
                param[1] = new DbParam("@BranchCode", branch.BranchCode, SqlDbType.VarChar);
                param[2] = new DbParam("@BranchName", branch.BranchName, SqlDbType.VarChar);
                param[3] = new DbParam("@Address", branch.Address, SqlDbType.VarChar);
                param[4] = new DbParam("@PhoneNo", branch.PhoneNo, SqlDbType.VarChar);
                param[5] = new DbParam("@City", branch.City, SqlDbType.VarChar);
                param[6] = new DbParam("@State", branch.State, SqlDbType.VarChar);
                param[7] = new DbParam("@IsActive", branch.IsActive, SqlDbType.Bit);
                param[8] = new DbParam("@CreatedBy", StaffId, SqlDbType.Int);

                RoleId = Db.Insert("SP_BRANCH_INSUPD", param,true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return RoleId;
        }

        public Branch BranchById(int branchId)
        {
            Branch branch = null;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@BranchId", branchId, SqlDbType.Int);

                DataRow row = Db.GetDataRow("SP_BRANCH_BY_ID", param);

                if (row != null)
                {
                    branch = new Branch();

                    branch.BranchId = Db.ToInteger(row["BranchId"]);
                    branch.BranchCode = Db.ToString(row["BranchCode"]);
                    branch.BranchName = Db.ToString(row["BranchName"]);
                    branch.Address = Db.ToString(row["Address"]);
                    branch.PhoneNo = Db.ToString(row["PhoneNo"]);
                    branch.City = Db.ToString(row["City"]);
                    branch.State = Db.ToString(row["State"]);
                    branch.IsActive = Db.ToBoolean(row["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return branch;
        }

        public int DeleteBranchById(int branchId,int staffId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[2];

                param[0] = new DbParam("@BranchId", branchId, SqlDbType.Int);
                param[1] = new DbParam("@StaffId", staffId, SqlDbType.Int);

                result =Db.Insert("SP_BRANCH_DELETE_BY_ID", param,false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }

        public IList<Branch> BranchesByUser(int UserId, string UserRole)
        {
            IList<Branch> branches = null;

            var param = new DbParam[2];

            param[0] = new DbParam("@UserId", UserId, SqlDbType.Int);
            param[1] = new DbParam("@UserRole", UserRole, SqlDbType.VarChar);

            DataTable dt = Db.GetDataTable("SP_BRANCES_BY_USER", param);

            if (dt != null && dt.Rows.Count > 0)
            {
                branches = new List<Branch>();
                foreach (DataRow dr in dt.Rows)
                {
                    Branch branch = new Branch();

                    branch.BranchId = Db.ToInteger(dr["BranchId"]);
                    branch.BranchCode = Db.ToString(dr["BranchCode"]);
                    branch.BranchName = Db.ToString(dr["BranchName"]);

                    branches.Add(branch);
                }
            }

            return branches;
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
