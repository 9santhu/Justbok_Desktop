using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;

namespace JustbokApplication.Data
{
    public static class Db
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


        /// <summary>
        /// Executes Update statements in the database.
        /// </summary>
        /// <param name="spName">string</param>
        /// <param name="spParams">array of object</param>
        /// <param name="getId">bool</param>
        /// <returns>int</returns>
        public static int Update(string spName, DbParam[] spParams, bool getId)
        {
            using (DbConnection connection = (new SqlConnection()))
            {
                var retValue = 0;
                connection.ConnectionString = connectionString;

                using (DbCommand command = (new SqlCommand()))
                {
                    command.Connection = connection;
                    command.CommandText = spName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 1800;

                    if (spParams != null)
                    {
                        AssignParameters(command, spParams);
                    }

                    connection.Open();

                    try
                    {
                        if (getId)
                        {
                            var spParameter = new SqlParameter("lngReturn", SqlDbType.Int);
                            spParameter.Direction = ParameterDirection.ReturnValue;
                            command.Parameters.Add(spParameter);

                            command.ExecuteNonQuery();
                            retValue = int.Parse(spParameter.Value.ToString());
                        }
                        else
                        {
                            retValue = command.ExecuteNonQuery();
                        }
                        AssignOutputParameters(command, spParams);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }

                        connection.Dispose();
                    }

                    return retValue;
                }
            }
        }

        /// <summary>
        /// Executes update statement in database
        /// </summary>
        /// <param name="ds">System.Data.DataSet</param>
        /// <param name="spInsert">string</param>
        /// <param name="spUpdate">string</param>
        /// <param name="spDelete">string</param>
        /// <param name="spParams">array of object</param>
        /// <param name="getId">bool</param>
        /// <returns>bool</returns>
        public static int DSUpdate(DataSet ds, string spInsert, string spUpdate, string spDelete, DbParam[] spParams, bool getId)
        {
            var adapter = new SqlDataAdapter();
            var blnReturnValue = 0;

            using (var connection = (new SqlConnection()))
            {
                connection.ConnectionString = connectionString;

                adapter.InsertCommand = (SqlCommand)AssignParameters(new SqlCommand(spInsert), spParams);
                adapter.UpdateCommand = (SqlCommand)AssignParameters(new SqlCommand(spUpdate), spParams);
                adapter.DeleteCommand = (SqlCommand)AssignParameters(new SqlCommand(spDelete), spParams);

                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;
                adapter.DeleteCommand.CommandType = CommandType.StoredProcedure;

                adapter.InsertCommand.Connection = connection;
                adapter.UpdateCommand.Connection = connection;
                adapter.DeleteCommand.Connection = connection;
                try
                {
                    connection.Open();
                    blnReturnValue = adapter.Update(ds, ds.Tables[0].TableName);
                }
                catch (Exception ex)
                {
                    blnReturnValue = -1;
                    throw ex;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }

            return blnReturnValue;
        }

        /// <summary>
        /// Executes update statement in database
        /// </summary>
        /// <param name="ds">System.Data.DataSet</param>
        /// <param name="spInsert">string</param>
        /// <param name="spUpdate">string</param>
        /// <param name="spDelete">string</param>
        /// <param name="spParams">array of object</param>
        /// <param name="getId">bool</param>
        /// <returns>bool</returns>
        public static bool Update(DataSet ds, string spInsert, string spUpdate, string spDelete, DbParam[] spParams, bool getId)
        {
            var adapter = new SqlDataAdapter();
            var blnReturnValue = true;

            using (var connection = (new SqlConnection()))
            {
                connection.ConnectionString = connectionString;

                adapter.InsertCommand = (SqlCommand)AssignParameters(new SqlCommand(spInsert), spParams);
                adapter.UpdateCommand = (SqlCommand)AssignParameters(new SqlCommand(spUpdate), spParams);
                adapter.DeleteCommand = (SqlCommand)AssignParameters(new SqlCommand(spDelete), spParams);

                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;
                adapter.DeleteCommand.CommandType = CommandType.StoredProcedure;

                adapter.InsertCommand.Connection = connection;
                adapter.UpdateCommand.Connection = connection;
                adapter.DeleteCommand.Connection = connection;
                try
                {
                    connection.Open();
                    var returnValue = adapter.Update(ds, ds.Tables[0].TableName);
                    if (returnValue == -1)
                    {
                        blnReturnValue = false;
                    }
                }
                catch (Exception ex)
                {
                    blnReturnValue = false;
                    throw ex;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }

            return blnReturnValue;
        }

        /// <summary>
        /// Executes Update statements in the database.
        /// </summary>
        /// <param name="spName">string</param>
        /// <param name="spParams">string</param>
        /// <returns>int</returns>
        public static int Update(string spName, DbParam[] spParams)
        {
            return Update(spName, spParams, false);
        }

        /// <summary>
        /// Executes Insert statements in the database. Optionally returns new identifier.
        /// </summary>
        /// <param name="spName">string</param>
        /// <param name="spParams">array of object</param>
        /// <param name="getId">bool</param>
        /// <returns>int</returns>
        public static int Insert(string spName, DbParam[] spParams, bool getId)
        {
            using (DbConnection connection = (new SqlConnection()))
            {
                var retValue = 0;
                connection.ConnectionString = connectionString;

                using (DbCommand command = (new SqlCommand()))
                {
                    command.Connection = connection;
                    command.CommandTimeout = 1800;
                    command.CommandText = spName;
                    command.CommandType = CommandType.StoredProcedure;

                    if (spParams != null)
                    {
                        AssignParameters(command, spParams);
                    }

                    connection.Open();

                    try
                    {
                        if (getId)
                        {
                            var spParameter = new SqlParameter("lngReturn", SqlDbType.Int);
                            spParameter.Direction = ParameterDirection.ReturnValue;
                            command.Parameters.Add(spParameter);

                            command.ExecuteNonQuery();
                            retValue = int.Parse(spParameter.Value.ToString());
                        }
                        else
                        {
                            retValue = command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }

                        connection.Dispose();
                    }

                    return retValue;
                }
            }
        }


        /// <summary>
        /// Executes Insert statements in the database. Optionally returns new identifier.
        /// </summary>
        /// <param name="spName">string</param>
        /// <param name="spParams">array of object</param>
        /// <param name="getId">bool</param>
        /// <returns>string</returns>
        public static string strInsert(string spName, DbParam[] spParams, bool getId)
        {
            using (DbConnection connection = (new SqlConnection()))
            {
                var retValue = string.Empty;
                connection.ConnectionString = connectionString;

                using (DbCommand command = (new SqlCommand()))
                {
                    command.Connection = connection;
                    command.CommandTimeout = 1800;
                    command.CommandText = spName;
                    command.CommandType = CommandType.StoredProcedure;

                    if (spParams != null)
                    {
                        AssignParameters(command, spParams);
                    }

                    connection.Open();

                    try
                    {
                        if (getId)
                        {
                            var spParameter = new SqlParameter("lngReturn", SqlDbType.VarChar);
                            spParameter.Direction = ParameterDirection.ReturnValue;
                            command.Parameters.Add(spParameter);

                            command.ExecuteNonQuery();
                            retValue = spParameter.Value.ToString();
                        }
                        else
                        {
                            retValue = command.ExecuteNonQuery().ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }

                        connection.Dispose();
                    }

                    return retValue;
                }
            }
        }

        /// <summary>
        /// Executes Insert statements in the database.
        /// </summary>
        /// <param name="spName">string</param>
        /// <param name="spParams">array of object</param>
        public static void Insert(string spName, DbParam[] spParams)
        {
            Insert(spName, spParams, false);
        }





        /// <summary>
        /// Populates a DataSet according to a stored procedure and parameters.
        /// </summary>
        /// <param name="spName">string</param>
        /// <param name="spParams">array of object</param>
        /// <returns>System.Data.DataSet</returns>
        public static DataSet GetDataSet(string spName, DbParam[] spParams)
        {
            return GetDataSet(spName, spParams, null);
        }

        /// <summary>
        /// overloaded method to populate a DataSet according to a stored procedure and parameters.
        /// </summary>
        /// <param name="spName">string</param>
        /// <param name="spParams">array of object</param>
        /// <param name="Timeout">int</param>
        /// <returns>System.Data.DataSet</returns>
        public static DataSet GetDataSet(string spName, DbParam[] spParams, int? Timeout)
        {
            using (DbConnection connection = (new SqlConnection()))
            {
                connection.ConnectionString = connectionString;

                using (DbCommand command = (new SqlCommand()))
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;

                    if (Timeout.HasValue)
                    {
                        command.CommandTimeout = Timeout.Value;
                    }
                    command.CommandTimeout = 1800;
                    if (spParams != null)
                    {
                        AssignParameters(command, spParams);
                    }

                    using (DbDataAdapter adapter = (new SqlDataAdapter()))
                    {
                        adapter.SelectCommand = command;

                        var ds = new DataSet();
                        adapter.Fill(ds);

                        return ds;
                    }
                }
            }
        }

        /// <summary>
        /// overloaded method to populate a DataTable according to a stored procedure and parameters.
        /// </summary>
        /// <param name="spName">string</param>
        /// <param name="spParams">array of object</param>
        /// <returns>System.Data.DataTable</returns>
        public static DataTable GetDataTable(string spName, DbParam[] spParams)
        {
            DataTable dt = null;
            var ds = GetDataSet(spName, spParams);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;
        }

        /// <summary>
        /// overloaded method to populate a DataRow according to a stored procedure and parameters.
        /// </summary>
        /// <param name="spName">string</param>
        /// <param name="spParams">array of object</param>
        /// <returns>System.Data.DataRow</returns>
        public static DataRow GetDataRow(string spName, DbParam[] spParams)
        {
            DataRow row = null;

            var dt = GetDataTable(spName, spParams);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    row = dt.Rows[0];
                }
            }

            return row;
        }

        /// <summary>
        /// Executes a stored procedure and returns a scalar value.
        /// </summary>
        /// <param name="spName">string</param>
        /// <param name="spParams">array of object</param>
        /// <returns>object</returns>
        public static object GetScalar(string spName, DbParam[] spParams)
        {
            using (DbConnection connection = (new SqlConnection()))
            {
                connection.ConnectionString = connectionString;

                using (DbCommand command = (new SqlCommand()))
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;

                    if (spParams != null)
                    {
                        AssignParameters(command, spParams);
                    }

                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }





        /// <summary>
        /// Assigns paramets to DbCommand
        /// </summary>
        /// <param name="command">System.Data.Common.DbCommand</param>
        /// <param name="spParams">array of object</param>
        /// <returns>System.Data.Common.DbCommand</returns>
        private static DbCommand AssignParameters(DbCommand command, DbParam[] spParams)
        {
            if (spParams != null)
            {
                foreach (DbParam spParam in spParams)
                {
                    var spParameter = new SqlParameter();

                    spParameter.SqlDbType = spParam.ParamType;
                    spParameter.ParameterName = spParam.ParamName;
                    spParameter.Value = spParam.ParamValue;
                    spParameter.Direction = spParam.ParamDirection;
                    spParameter.SourceColumn = spParam.ParamSourceColumn;
                    spParameter.Size = spParam.Size;

                    command.Parameters.Add(spParameter);
                }
            }

            return command;
        }

        /// <summary>
        /// Escapes an input string for database processing, that is, 
        /// surround it with quotes and change any quote in the string to 
        /// two adjacent quotes (i.e. escape it). 
        /// If input string is null or empty a NULL string is returned.
        /// </summary>
        /// <param name="s">Input string.</param>
        /// <returns>Escaped output string.</returns>
        public static string Escape(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return "NULL";
            }
            else
            {
                return "'" + s.Trim().Replace("'", "''") + "'";
            }
        }

        /// <summary>
        /// Escapes an input string for database processing, that is, 
        /// surround it with quotes and change any quote in the string to 
        /// two adjacent quotes (i.e. escape it). 
        /// Also trims string at a given maximum length.
        /// If input string is null or empty a NULL string is returned.
        /// </summary>
        /// <param name="s">Input string.</param>
        /// <param name="maxLength">Maximum length of output string.</param>
        /// <returns>Escaped output string.</returns>
        public static string Escape(string s, int maxLength)
        {
            if (String.IsNullOrEmpty(s))
            {
                return "NULL";
            }
            else
            {
                s = s.Trim();
                if (s.Length > maxLength)
                {
                    s = s.Substring(0, maxLength - 1);
                }
                return "'" + s.Trim().Replace("'", "''") + "'";
            }
        }

        /// <summary>
        /// converts an object to double value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>double</returns>
        public static double ToDouble(object value)
        {
            double retValue = 0;

            if (value != DBNull.Value)
            {
                double.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }

        /// <summary>
        /// converts an object to decimal
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(object value)
        {
            decimal retValue = 0;

            if (value != DBNull.Value)
            {
                decimal.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }

        /// <summary>
        /// converts an object to float
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>float</returns>
        public static float ToFloat(object value)
        {
            float retValue = 0;

            if (value != DBNull.Value)
            {
                float.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }
        /// <summary>
        /// converts an object to integer value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>int</returns>
        public static int ToInteger(object value)
        {
            var retValue = 0;

            if (value != DBNull.Value)
            {
                int.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }

        /// <summary>
        /// converts an object to long value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>System.Int64</returns>
        public static long ToLong(object value)
        {
            long retValue = 0;

            if (value != DBNull.Value)
            {
                long.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }

        /// <summary>
        /// converts an object to string value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>string</returns>
        public static string ToString(object value)
        {
            var retValue = string.Empty;

            if (value != DBNull.Value)
            {
                retValue = value.ToString();
            }

            return retValue;
        }

        /// <summary>
        /// converts an object to boolean value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>bool</returns>
        public static bool ToBoolean(object value)
        {
            var retValue = false;

            if (value != DBNull.Value)
            {
                try
                {
                    value = Convert.ToBoolean(value);
                }
                catch (Exception ex)
                {
                    CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
                }
                bool.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }

        /// <summary>
        /// converts an object to datetime value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>System.DateTime</returns>
        public static DateTime ToDateTime(object value)
        {
            var retValue = new DateTime();

            if (value != DBNull.Value)
            {
                DateTime.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }

        /// <summary>
        /// converts an object to bigint value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>Int32</returns>
        public static Int64 ToBigInteger(object value)
        {
            var retValue = 0;

            if (value != DBNull.Value)
            {
                try
                {
                    retValue = int.Parse(value.ToString());
                }
                catch (Exception ex)
                {
                    CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
                }
            }

            return retValue;
        }


        public static bool IsDataExists(DataSet ds)
        {
            var returnValue = false;
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        returnValue = true;
                    }
                }
            }
            return returnValue;
        }

        public static string ToFormatedDatetime(object value)
        {
            var ReturnString = string.Empty;
            try
            {
                if (value != DBNull.Value && value != null)
                {
                    ReturnString = Convert.ToDateTime(value).ToString("MMM dd, yyyy");
                }
            }
            catch
            {
                ReturnString = string.Empty;
            }
            return ReturnString;
        }

        public static float ToFloatValue(object value)
        {
            float retValue = 0;

            if (value != DBNull.Value)
            {
                float.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }

        public static string GetCurrencyCode(object value, string type)
        {
            var retValue = string.Empty;

            if (value != DBNull.Value && value != null)
            {
                switch (value.ToString())
                {
                    case "UAE Dirham":
                        if (type == "0")
                        {
                            retValue = "AED";
                        }
                        else
                        {
                            retValue = "Fills";
                        }
                        break;
                    case "Omani Riyal":
                        if (type == "0")
                        {
                            retValue = "Riyal";
                        }
                        else
                        {
                            retValue = "Fills";
                        }
                        break;
                    case "Pakistani Rupees":
                        if (type == "0")
                        {
                            retValue = "Rupees";
                        }
                        else
                        {
                            retValue = "Paisa";
                        }
                        break;
                    case "Bahrain Dinar":
                        if (type == "0")
                        {
                            retValue = "Dinar";
                        }
                        else
                        {
                            retValue = "Dinar";
                        }
                        break;
                    case "Other":
                        retValue = "";
                        break;
                }
            }

            return retValue;
        }

        public static float Truncate(this float value, int digits)
        {
            var result = Math.Round(value, 2);
            return (float)result;
        }

        private static DbCommand AssignOutputParameters(DbCommand command, DbParam[] spParams)
        {
            if (spParams != null)
            {
                if (spParams.Length > 0)
                {
                    var outparams = from c in spParams
                                    where c.ParamDirection == ParameterDirection.InputOutput ||
                                        c.ParamDirection == ParameterDirection.Output
                                    select c;

                    foreach (DbParam param in outparams)
                    {
                        if (command.Parameters[param.ParamName] != null)
                        {
                            param.ParamValue = command.Parameters[param.ParamName].Value;
                        }
                    }
                }
            }
            return command;
        }

        public static bool BatchInsert(DataTable dt, string spName, DbParam[] spParams)
        {
            var daUpdate = new SqlDataAdapter();
            using (DbConnection connection = (new SqlConnection()))
            {
                connection.ConnectionString = connectionString;
                using (DbCommand command = (new SqlCommand()))
                {
                    command.Connection = connection;
                    command.CommandText = spName;
                    command.CommandType = CommandType.StoredProcedure;

                    if (spParams != null)
                    {
                        AssignParameters(command, spParams);
                    }

                    daUpdate.InsertCommand = (SqlCommand)command;
                    connection.Open();
                    try
                    {
                        if (dt.GetChanges(DataRowState.Added) != null)
                        {
                            daUpdate.Update(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Dispose();
                    }
                }
            }
            if (dt.GetChanges(DataRowState.Added) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
