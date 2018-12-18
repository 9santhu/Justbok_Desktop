using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
     public class RoleDao
    {
        public Result GetRoles(string roleSearch,string orderBy,string direction, int startRowIndex, int maximumRows)
        {
            Result objResult = new Result();
            try
            {
                var param = new DbParam[5];

                param[0] = new DbParam("@SortBy", orderBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SortDirection", direction, SqlDbType.VarChar);
                param[2] = new DbParam("@StartRowIndex", startRowIndex, SqlDbType.Int);
                param[3] = new DbParam("@MaximumRows", maximumRows, SqlDbType.Int);
                param[4] = new DbParam("@RoleSearch", roleSearch, SqlDbType.VarChar);

                DataSet ds = Db.GetDataSet("SP_ROLE_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<Role> lstRoles = new List<Role>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Role objRole = new Role();
                        objRole.RoleId = Db.ToInteger(row["RoleId"]);
                        objRole.RoleName = Db.ToString(row["RoleName"]);
                        lstRoles.Add(objRole);
                    }

                    objResult.Items = lstRoles;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["RolesCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int RoleInsUpd(Role objRole,int StaffId)
        {
            int RoleId = 0;
            try
            {
                var param = new DbParam[4];

                param[0] = new DbParam("@RoleId", objRole.RoleId, SqlDbType.Int);
                param[1] = new DbParam("@RoleName", objRole.RoleName, SqlDbType.VarChar);
                param[2] = new DbParam("@IsActive", objRole.IsActive, SqlDbType.Bit);
                param[3] = new DbParam("@CreatedBy", StaffId, SqlDbType.Int);

                RoleId = Db.Insert("SP_ROLE_INSUPD", param,true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return RoleId;
        }

        public Role RoleById(int roleId)
        {
            Role objRole = null;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@RoleId", roleId, SqlDbType.Int);

                DataRow dr = Db.GetDataRow("SP_ROLE_BY_ID", param);

                if (dr != null)
                {
                    objRole = new Role();

                    objRole.RoleId = Db.ToInteger(dr["RoleId"]);
                    objRole.RoleName = Db.ToString(dr["RoleName"]);
                    objRole.IsActive = Db.ToBoolean(dr["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objRole;
        }

        public int DeleteRoleById(int roleId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@RoleId", roleId, SqlDbType.Int);

                result=Db.Insert("SP_ROLE_DELETE_BY_ID", param,false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }

        public IList<Role> GetActiveRoles()
        {
            IList<Role> roles = null;
            try
            {
                DataTable dt = Db.GetDataTable("SP_ROLE_ACTIVE", null);

                if (dt != null && dt.Rows.Count>0)
                {
                    roles = new List<Role>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        Role role = new Role();
                        role.RoleId = Db.ToInteger(dr["RoleId"]);
                        role.RoleName = Db.ToString(dr["RoleName"]);
                        roles.Add(role);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return roles;
        }
    }
}
