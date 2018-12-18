using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
     public class LoginDao
    {
        public User Login(string username,string password)
        {
            User objUser = null;
            try
            {
                var param = new DbParam[2];

                param[0] = new DbParam("@username", username, SqlDbType.VarChar);
                param[1] = new DbParam("@password", password, SqlDbType.VarChar);

                DataTable dt = Db.GetDataTable("sp_login", param);

                if (dt != null && dt.Rows.Count > 0)
                {
                    objUser = new User();
                    foreach (DataRow row in dt.Rows)
                    {
                        objUser.UserId = Db.ToInteger(row["UserId"]);
                        objUser.FirstName = Db.ToString(row["FirstName"]);
                        objUser.LastName = Db.ToString(row["LastName"]);
                        objUser.RoleName = Db.ToString(row["RoleName"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objUser;
        }
    }
}
