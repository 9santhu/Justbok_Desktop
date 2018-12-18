using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
    public class PackageDao
    {
        public Result GetPackages(string nameSearch, int monthSearch,float amountSearch,
                                  string orderBy, string direction,int startRowIndex, int maximumRows,int branchId)
        {
            Result objResult = new Result();
            try
            {
                var param = new DbParam[8];

                param[0] = new DbParam("@SortBy", orderBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SortDirection", direction, SqlDbType.VarChar);
                param[2] = new DbParam("@StartRowIndex", startRowIndex, SqlDbType.Int);
                param[3] = new DbParam("@MaximumRows", maximumRows, SqlDbType.Int);
                param[4] = new DbParam("@NameSearch", nameSearch, SqlDbType.VarChar);
                param[5] = new DbParam("@MonthSearch", monthSearch, SqlDbType.Int);
                param[6] = new DbParam("@Amount", amountSearch, SqlDbType.Decimal);
                param[7] = new DbParam("@BranchId", branchId, SqlDbType.Int);

                DataSet ds = Db.GetDataSet("SP_PACKAGE_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<Package> packages = new List<Package>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Package package = new Package();
                        package.PackageId = Db.ToInteger(row["PackageId"]);
                        package.Name = Db.ToString(row["Name"]);
                        package.Months = Db.ToString(row["Months"]);
                        package.Amount = Math.Round(Db.ToDecimal(row["Amount"]),2).ToString();
                        package.MinAmount = Math.Round(Db.ToDecimal(row["MinAmount"]), 2).ToString();
                        package.Category = new Category();
                        package.Category.CategoryName = Db.ToString(row["CategoryName"]);
                        packages.Add(package);
                    }

                    objResult.Items = packages;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["PackageCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int PackageInsUpd(Package package, int StaffId)
        {
            int SupplierId = 0;
            try
            {
                var param = new DbParam[9];

                param[0] = new DbParam("@PackageId", package.PackageId, SqlDbType.Int);
                param[1] = new DbParam("@Name", package.Name, SqlDbType.VarChar);
                param[2] = new DbParam("@Months", package.Months, SqlDbType.Int);
                param[3] = new DbParam("@Amount", package.Amount, SqlDbType.Decimal);
                param[4] = new DbParam("@MinAmount", package.MinAmount, SqlDbType.Decimal);
                param[5] = new DbParam("@CategoryId",package.Category.CategoryId, SqlDbType.Int);
                param[6] = new DbParam("@IsActive", package.IsActive, SqlDbType.Bit);
                param[7] = new DbParam("@CreatedBy", StaffId, SqlDbType.Int);
                param[8] = new DbParam("@BranchId", package.BranchId, SqlDbType.Int);

                SupplierId = Db.Insert("SP_PACKAGE_INSUPD", param, true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return SupplierId;
        }

        public Package PackageById(int packageID)
        {
            Package package = null;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@PackageId", packageID, SqlDbType.Int);

                DataRow row = Db.GetDataRow("SP_PACKAGE_BY_ID", param);

                if (row != null)
                {
                    package = new Package();

                    package.PackageId = Db.ToInteger(row["PackageId"]);
                    package.Name = Db.ToString(row["Name"]);
                    package.Months = Db.ToString(row["Months"]);
                    package.Amount = Math.Round(Db.ToDecimal(row["Amount"]),2).ToString();
                    package.Category = new Category();
                    package.Category.CategoryId= Db.ToInteger(row["CategoryId"]);
                    package.Category.CategoryName= Db.ToString(row["CategoryName"]);
                    package.MinAmount = Math.Round(Db.ToDecimal(row["MinAmount"]), 2).ToString();
                    package.IsActive = Db.ToBoolean(row["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return package;
        }

        public int DeletePackageById(int packageId, int staffId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[2];

                param[0] = new DbParam("@PackageId", packageId, SqlDbType.Int);
                param[1] = new DbParam("@StaffId", staffId, SqlDbType.Int);

                result = Db.Insert("SP_PACKAGE_DELETE_BY_ID", param, false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }
    }
}
