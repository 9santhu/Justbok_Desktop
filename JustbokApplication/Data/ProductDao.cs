using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
     public class ProductDao
    {
        public Result GetProducts(string brandSearch,string prodcutSearch,string orderBy,string direction, 
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
                param[4] = new DbParam("@BrandSearch", brandSearch, SqlDbType.VarChar);
                param[5] = new DbParam("@ProductSearch", prodcutSearch, SqlDbType.VarChar);
                param[6] = new DbParam("@BranchId", branchId, SqlDbType.Int);
                DataSet ds = Db.GetDataSet("SP_PRODUCT_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<Product> products = new List<Product>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Product product = new Product();
                        product.ProductId = Db.ToInteger(row["ProductId"]);
                        product.ProductName = Db.ToString(row["ProductName"]);
                        product.BrandName = Db.ToString(row["BrandName"]);
                        product.Price = Db.ToString(row["Price"]);
                        product.LowStockQuantity = Db.ToInteger(row["LowStockQuantity"]);
                        product.Description = Db.ToString(row["Description"]);
                        product.ForSale = Db.ToBoolean(row["ForSale"]);
                        products.Add(product);
                    }

                    objResult.Items = products;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["ProductCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int ProductInsUpd(Product product,int StaffId)
        {
            int ProductId = 0;
            try
            {
                var param = new DbParam[11];
                
                param[0] = new DbParam("@ProductId", product.ProductId, SqlDbType.Int);
                param[1] = new DbParam("@BrandName", product.BrandName, SqlDbType.VarChar);
                param[2] = new DbParam("@ProductName", product.ProductName, SqlDbType.VarChar);
                param[3] = new DbParam("@Price", product.Price, SqlDbType.Float);
                param[4] = new DbParam("@Quantity", product.Quantity, SqlDbType.BigInt);
                param[5] = new DbParam("@Description", product.Description, SqlDbType.VarChar);
                param[6] = new DbParam("@LowStockQuantity", product.LowStockQuantity, SqlDbType.Int);
                param[7] = new DbParam("@ForSale", product.ForSale, SqlDbType.Bit);
                param[8] = new DbParam("@IsActive", product.IsActive, SqlDbType.Bit);
                param[9] = new DbParam("@BranchId", product.BranchId, SqlDbType.Int);
                param[10] = new DbParam("@CreatedBy", StaffId, SqlDbType.Int);

                ProductId = Db.Insert("SP_PRODUCT_INSUPD", param,true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return ProductId;
        }

        public Product ProductById(int productId)
        {
            Product product = null;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@ProductId", productId, SqlDbType.Int);

                DataRow row = Db.GetDataRow("SP_PRODUCT_BY_ID", param);

                if (row != null)
                {
                    product = new Product();

                    product.ProductId = Db.ToInteger(row["ProductId"]);
                    product.ProductName = Db.ToString(row["ProductName"]);
                    product.BrandName = Db.ToString(row["BrandName"]);
                    product.Price = Db.ToString(row["Price"]);
                    product.Quantity = Db.ToInteger(row["Quantity"]);
                    product.Description = Db.ToString(row["Description"]);
                    product.LowStockQuantity = Db.ToInteger(row["LowStockQuantity"]);
                    product.ForSale = Db.ToBoolean(row["ForSale"]);
                    product.IsActive = Db.ToBoolean(row["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return product;
        }

        public int DeleteProductById(int productId,int staffId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[2];

                param[0] = new DbParam("@ProductId", productId, SqlDbType.Int);
                param[1] = new DbParam("@StaffId", staffId, SqlDbType.Int);

                result =Db.Insert("SP_PRODUCT_DELETE_BY_ID", param,false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }

    }
}
