using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
     public class StaffDao
    {
        public Result GetStaff(string nameSearch,string roleSearch,string mailSearch,string phoneSearch,
            string orderBy,string direction, int startRowIndex, int maximumRows,int branchId)
        {
            Result objResult = new Result(); ;
            try
            {
                var param = new DbParam[9];

                param[0] = new DbParam("@StaffSearch", nameSearch, SqlDbType.VarChar);
                param[1] = new DbParam("@RoleSearch", roleSearch, SqlDbType.VarChar);
                param[2] = new DbParam("@MailSearch", mailSearch, SqlDbType.VarChar);
                param[3] = new DbParam("@PhoneSearch", phoneSearch, SqlDbType.VarChar);
                param[4] = new DbParam("@SortBy", orderBy, SqlDbType.VarChar);
                param[5] = new DbParam("@SortDirection", direction, SqlDbType.VarChar);
                param[6] = new DbParam("@MaximumRows", maximumRows, SqlDbType.Int);
                param[7] = new DbParam("@StartRowIndex", startRowIndex, SqlDbType.Int);
                param[8] = new DbParam("@BranchId", branchId, SqlDbType.Int);

                DataSet ds = Db.GetDataSet("SP_STAFF_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<Staff> staffs = new List<Staff>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Staff staff = new Staff();
                        staff.UserId = Db.ToInteger(row["UserId"]);
                        staff.UserName = Db.ToString(row["UserName"]);
                        staff.FirstName = Db.ToString(row["FirstName"]);
                        staff.LastName = Db.ToString(row["LastName"]);
                        staff.Email = Db.ToString(row["Email"]);
                        staff.PhoneNo = Db.ToString(row["PhoneNo"]);
                        staff.Role = new Role();
                        staff.Role.RoleName = Db.ToString(row["RoleName"]);
                        staff.IsEdit = Db.ToBoolean(row["IsEdit"])==true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                        staff.IsDelete = Db.ToBoolean(row["IsDelete"]) == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; ;
                        staffs.Add(staff);
                    }

                    objResult.Items = staffs;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["UserCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int StaffInsUpd(Staff staff, int UserId)
        {
            int StaffId = 0;
            try
            {
                var param = new DbParam[14];

                param[0] = new DbParam("@UserId", staff.UserId, SqlDbType.Int);
                param[1] = new DbParam("@UserName", staff.UserName, SqlDbType.VarChar);
                param[2] = new DbParam("@FirstName", staff.FirstName, SqlDbType.VarChar);
                param[3] = new DbParam("@LastName", staff.LastName, SqlDbType.VarChar);
                param[4] = new DbParam("@DOB", staff.DOB, SqlDbType.Date);
                param[5] = new DbParam("@PhoneNo", staff.PhoneNo, SqlDbType.VarChar);
                param[6] = new DbParam("@Email", staff.Email, SqlDbType.VarChar);
                param[7] = new DbParam("@Address", staff.Address, SqlDbType.VarChar);
                param[8] = new DbParam("@RoleId", staff.Role.RoleId, SqlDbType.Int);
                param[9] = new DbParam("@DailySalary", staff.DailySalary, SqlDbType.Money);
                param[10] = new DbParam("@Branches", staff.strBranches, SqlDbType.VarChar);
                param[11] = new DbParam("@Shifts", staff.strShifts, SqlDbType.VarChar);
                param[12] = new DbParam("@IsActive", staff.IsActive, SqlDbType.Bit);
                param[13] = new DbParam("@CreatedBy", UserId, SqlDbType.Int);

                StaffId = Db.Insert("SP_USER_INSUPD", param, true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return StaffId;
        }

        public Staff StaffById(int staffId)
        {
            Staff staff = null;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@UserId", staffId, SqlDbType.Int);

                DataSet dataSet = Db.GetDataSet("SP_USER_BY_ID", param);

                if (dataSet != null && dataSet.Tables.Count>0 && dataSet.Tables[0].Rows.Count>0)
                {
                    staff = new Staff();
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        staff.UserId = Db.ToInteger(dr["UserId"]);
                        staff.UserName = Db.ToString(dr["UserName"]);
                        staff.FirstName = Db.ToString(dr["FirstName"]);
                        staff.LastName = Db.ToString(dr["LastName"]);
                        staff.DOB = Db.ToDateTime(dr["DOB"]);
                        staff.PhoneNo = Db.ToString(dr["PhoneNo"]);
                        staff.Email = Db.ToString(dr["Email"]);
                        staff.Address = Db.ToString(dr["Address"]);
                        staff.DailySalary = Db.ToDecimal(dr["DailySalary"]);
                        staff.IsActive = Db.ToBoolean(dr["IsActive"]);
                        staff.Role = new Role();
                        staff.Role.RoleId = Db.ToInteger(dr["RoleId"]);
                        staff.Role.RoleName = Db.ToString(dr["RoleName"]);
                    }

                    if (dataSet.Tables.Count > 1 && dataSet.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataSet.Tables[1].Rows)
                        {
                            staff.SelectedBranches.Add(new Node() { Id = Db.ToInteger(dr["BranchId"]), Name = Db.ToString(dr["BranchName"]) });
                        }
                    }

                    if (dataSet.Tables.Count > 2 && dataSet.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataSet.Tables[2].Rows)
                        {
                            staff.SelectedShifts.Add(new Node() { Id = Db.ToInteger(dr["ShiftId"]), Name = Db.ToString(dr["Name"]) });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return staff;
        }

        public int DeleteUserById(int userId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@UserId", userId, SqlDbType.Int);

                result = Db.Insert("SP_USER_DELETE_BY_ID", param, false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }
    }
}
