using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
    public class SupplierDao
    {
        public Result GetSuppliers(string companySearch, string supplierSearch, string orderBy, string direction,
                                  int startRowIndex, int maximumRows,int branchId)
        {
            Result objResult = new Result();
            try
            {
                var param = new DbParam[7];

                param[0] = new DbParam("@SortBy", orderBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SortDirection", direction, SqlDbType.VarChar);
                param[2] = new DbParam("@StartRowIndex", startRowIndex, SqlDbType.Int);
                param[3] = new DbParam("@MaximumRows", maximumRows, SqlDbType.Int);
                param[4] = new DbParam("@CompanySearch", companySearch, SqlDbType.VarChar);
                param[5] = new DbParam("@SupplierSearch", supplierSearch, SqlDbType.VarChar);
                param[6] = new DbParam("@BranchId", branchId, SqlDbType.Int);

                DataSet ds = Db.GetDataSet("SP_SUPPLIER_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<Supplier> suppliers = new List<Supplier>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Supplier supplier = new Supplier();
                        supplier.SupplierId = Db.ToInteger(row["SupplierId"]);
                        supplier.CompanyName = Db.ToString(row["CompanyName"]);
                        supplier.SupplierName = Db.ToString(row["SupplierName"]);
                        supplier.RegistrationNo = Db.ToString(row["RegistrationNo"]);
                        suppliers.Add(supplier);
                    }

                    objResult.Items = suppliers;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["SupplierCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int SupplierInsUpd(Supplier supplier, int StaffId)
        {
            int SupplierId = 0;
            try
            {
                var param = new DbParam[12];

                param[0] = new DbParam("@SupplierId", supplier.SupplierId, SqlDbType.Int);
                param[1] = new DbParam("@CompanyName", supplier.CompanyName, SqlDbType.VarChar);
                param[2] = new DbParam("@RegistrationNo", supplier.RegistrationNo, SqlDbType.VarChar);
                param[3] = new DbParam("@FirstName", supplier.FirstName, SqlDbType.VarChar);
                param[4] = new DbParam("@LastName", supplier.LastName, SqlDbType.VarChar);
                param[5] = new DbParam("@Email", supplier.Email, SqlDbType.VarChar);
                param[6] = new DbParam("@PhoneNo", supplier.PhoneNo, SqlDbType.VarChar);
                param[7] = new DbParam("@FaxNo", supplier.FaxNo, SqlDbType.VarChar);
                param[8] = new DbParam("@Address", supplier.Address, SqlDbType.VarChar);
                param[9] = new DbParam("@IsActive", supplier.IsActive, SqlDbType.Bit);
                param[10] = new DbParam("@BranchId", supplier.BranchId, SqlDbType.Int);
                param[11] = new DbParam("@CreatedBy", StaffId, SqlDbType.Int);

                SupplierId = Db.Insert("SP_SUPPLIER_INSUPD", param, true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return SupplierId;
        }

        public Supplier SupplierById(int supplierId)
        {
            Supplier supplier = null;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@SupplierId", supplierId, SqlDbType.Int);

                DataRow row = Db.GetDataRow("SP_SUPPLIER_BY_ID", param);

                if (row != null)
                {
                    supplier = new Supplier();

                    supplier.SupplierId = Db.ToInteger(row["SupplierId"]);
                    supplier.CompanyName = Db.ToString(row["CompanyName"]);
                    supplier.RegistrationNo = Db.ToString(row["RegistrationNo"]);
                    supplier.FirstName = Db.ToString(row["FirstName"]);
                    supplier.LastName = Db.ToString(row["LastName"]);
                    supplier.Email = Db.ToString(row["Email"]);
                    supplier.PhoneNo = Db.ToString(row["PhoneNo"]);
                    supplier.FaxNo = Db.ToString(row["FaxNo"]);
                    supplier.Address = Db.ToString(row["Address"]);
                    supplier.IsActive = Db.ToBoolean(row["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return supplier;
        }

        public int DeleteSupplierById(int supplierId, int staffId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[2];

                param[0] = new DbParam("@SupplierId", supplierId, SqlDbType.Int);
                param[1] = new DbParam("@StaffId", staffId, SqlDbType.Int);

                result = Db.Insert("SP_SUPPLIER_DELETE_BY_ID", param, false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }

    }
}
