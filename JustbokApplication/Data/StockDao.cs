using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
     public class StockDao
    {
        public Result GetStock(string brandSearch, string prodcutSearch, string orderBy, string direction,
                                  int startRowIndex, int maximumRows, int branchId)
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

                DataSet ds = Db.GetDataSet("SP_STOCK_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<Stock> stocks = new List<Stock>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Stock stock = new Stock();
                        stock.ProductId = Db.ToInteger(row["ProductId"]);
                        stock.ProductName = Db.ToString(row["ProductName"]);
                        stock.BrandName = Db.ToString(row["BrandName"]);
                        stock.CurrentStock = Db.ToInteger(row["CurrentStock"]);
                        stock.IsVisible = System.Windows.Visibility.Collapsed;
                        stocks.Add(stock);
                    }

                    objResult.Items = stocks;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["StockCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int StockIns(DataTable stock)
        {
            int result = 0;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@Stock", stock, SqlDbType.Structured);

                result = Db.Insert("SP_STOCK_HISTORY_INS", param, true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
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

        public int DeleteProductById(int productId, int staffId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[2];

                param[0] = new DbParam("@ProductId", productId, SqlDbType.Int);
                param[1] = new DbParam("@StaffId", staffId, SqlDbType.Int);

                result = Db.Insert("SP_PRODUCT_DELETE_BY_ID", param, false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }

        public Result GetStockHistory(DateTime fromDate,DateTime toDate,int stockType,string orderBy, string direction,
                                  int startRowIndex, int maximumRows, int branchId)
        {
            Result objResult = new Result();
            try
            {
                var param = new DbParam[8];

                param[0] = new DbParam("@SortBy", orderBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SortDirection", direction, SqlDbType.VarChar);
                param[2] = new DbParam("@StartRowIndex", startRowIndex, SqlDbType.Int);
                param[3] = new DbParam("@MaximumRows", maximumRows, SqlDbType.Int);
                param[4] = new DbParam("@FromDate", fromDate, SqlDbType.DateTime);
                param[5] = new DbParam("@ToDate", toDate, SqlDbType.DateTime);
                param[6] = new DbParam("@StockType", stockType, SqlDbType.Int);
                param[7] = new DbParam("@BranchId", branchId, SqlDbType.Int);

                DataSet ds = Db.GetDataSet("SP_STOCKHISTORY_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<Stock> stocks = new List<Stock>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Stock stock = new Stock();
                        stock.ProductName = Db.ToString(row["ProductName"]);
                        stock.BrandName = Db.ToString(row["BrandName"]);
                        stock.CurrentStock = Db.ToInteger(row["CurrentStock"]);
                        stock.AddedQuantity = Db.ToInteger(row["Stock"]);
                        stock.StockDate = Db.ToDateTime(row["StockDate"]);
                        stocks.Add(stock);
                    }

                    objResult.Items = stocks;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["StockHistoryCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

    }
}
