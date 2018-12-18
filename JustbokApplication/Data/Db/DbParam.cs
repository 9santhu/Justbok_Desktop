using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace JustbokApplication.Data
{
    public class DbParam
    {
        private object _ParamValue;

        /// <summary>
        /// default constructor for DbParam class
        /// </summary>
        public DbParam()
        {
        }

        /// <summary>
        /// overloaded constructor for DbParam class
        /// </summary>
        /// <param name="paramName">Parameter Name</param>
        /// <param name="paramValue">Parameter Value</param>
        /// <param name="paramType">Parameter Type</param>
        public DbParam(string paramName, object paramValue, SqlDbType paramType)
            : this()
        {
            ParamDirection = ParameterDirection.Input;
            ParamName = paramName;
            ParamType = paramType;
            ParamValue = paramValue;
        }

        /// <summary>
        /// overloaded constructor for DbParam class
        /// </summary>
        /// <param name="paramName">Parameter Name</param>
        /// <param name="paramValue">Parameter Value</param>
        /// <param name="paramSourceColumn">Parameter Source Columns</param>
        /// <param name="paramType">Parameter Type</param>
        public DbParam(string paramName, string paramValue, string paramSourceColumn, SqlDbType paramType)
            : this(paramName, paramValue, paramType)
        {
            ParamSourceColumn = paramSourceColumn;
        }

        /// <summary>
        /// overloaded constructor for DbParam class
        /// </summary>
        /// <param name="paramName">Parameter Name</param>
        /// <param name="paramValue">Parameter Value</param>
        /// <param name="paramType">Parameter Type</param>
        /// <param name="paramDirection">Parameter Direction</param>
        public DbParam(string paramName, string paramValue, SqlDbType paramType, ParameterDirection paramDirection)
            : this(paramName, paramValue, paramType)
        {
            ParamDirection = paramDirection;
        }

        /// <summary>
        /// overloaded constructor for DbParam class
        /// </summary>
        /// <param name="paramName">Parameter Name</param>
        /// <param name="paramValue">Parameter Value</param>
        /// <param name="paramSourceColumn">Parameter Source Columns</param>
        /// <param name="paramType">Parameter Type</param>
        /// <param name="paramDirection">Parameter Direction</param>
        /// <param name="Size">Size</param>
        public DbParam(string paramName, string paramValue, string paramSourceColumn, SqlDbType paramType, ParameterDirection paramDirection, int Size)
            : this(paramName, paramValue, paramType, paramDirection)
        {
            ParamSourceColumn = paramSourceColumn;
            this.Size = Size;
        }
        /// <summary>
        /// overloaded constructor for DbParam class
        /// </summary>
        /// <param name="paramName">Parameter Name</param>
        /// <param name="paramValue">Parameter Value</param>
        /// <param name="paramSourceColumn">Parameter Source Columns</param>
        /// <param name="paramType">Parameter Type</param>
        /// <param name="paramDirection">Parameter Direction</param>
        /// <param name="Size">Size</param>
        public DbParam(string paramName, string paramValue, string paramSourceColumn, SqlDbType paramType, ParameterDirection paramDirection)
            : this(paramName, paramValue, paramType, paramDirection)
        {
            ParamSourceColumn = paramSourceColumn;
        }
        /// <summary>
        /// gets or sets the parameter name
        /// </summary>
        public string ParamName { get; set; }

        /// <summary>
        /// gets or sets the parameter value
        /// </summary>
        public object ParamValue
        {
            get
            {
                return _ParamValue;
            }
            set
            {
                if (value == null)
                {
                    _ParamValue = DBNull.Value;
                }
                else
                {
                    _ParamValue = value;
                }
            }
        }

        /// <summary>
        /// gets or sets the parameter source column
        /// </summary>
        public string ParamSourceColumn { get; set; }

        /// <summary>
        /// gets or sets the parameter type
        /// </summary>
        public SqlDbType ParamType { get; set; }

        /// <summary>
        /// gets or sets the parameter direction
        /// </summary>
        public ParameterDirection ParamDirection { get; set; }

        /// <summary>
        /// Get or set Size
        /// </summary>
        public int Size { get; set; }
    }
}

