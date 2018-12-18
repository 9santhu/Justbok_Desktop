using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
    public class CategoryDao
    {
        public Result GetCategories(string categorySearch, string descriptionSearch, string orderBy, string direction,
                                  int startRowIndex, int maximumRows)
        {
            Result objResult = new Result();
            try
            {
                var param = new DbParam[6];

                param[0] = new DbParam("@SortBy", orderBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SortDirection", direction, SqlDbType.VarChar);
                param[2] = new DbParam("@StartRowIndex", startRowIndex, SqlDbType.Int);
                param[3] = new DbParam("@MaximumRows", maximumRows, SqlDbType.Int);
                param[4] = new DbParam("@CategorySearch", categorySearch, SqlDbType.VarChar);
                param[5] = new DbParam("@DescriptionSearch", descriptionSearch, SqlDbType.VarChar);

                DataSet ds = Db.GetDataSet("SP_CATEGORY_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<Category> categories = new List<Category>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Category category = new Category();
                        category.CategoryId = Db.ToInteger(row["CategoryId"]);
                        category.CategoryName = Db.ToString(row["CategoryName"]);
                        category.Description = Db.ToString(row["Description"]);
                        category.IsActive = Db.ToBoolean(row["IsActive"]);
                        categories.Add(category);
                    }

                    objResult.Items = categories;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["CategoryCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int CategoryInsUpd(Category category, int StaffId)
        {
            int SupplierId = 0;
            try
            {
                var param = new DbParam[5];

                param[0] = new DbParam("@CategoryId", category.CategoryId, SqlDbType.Int);
                param[1] = new DbParam("@CategoryName", category.CategoryName, SqlDbType.VarChar);
                param[2] = new DbParam("@Description", category.Description, SqlDbType.VarChar);
                param[3] = new DbParam("@IsActive", category.IsActive, SqlDbType.Bit);
                param[4] = new DbParam("@CreatedBy", StaffId, SqlDbType.Int);

                SupplierId = Db.Insert("SP_CATEGORY_INSUPD", param, true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return SupplierId;
        }

        public Category CategoryById(int categoryId)
        {
            Category category = null;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@CategoryId", categoryId, SqlDbType.Int);

                DataRow row = Db.GetDataRow("SP_CATEGORY_BY_ID", param);

                if (row != null)
                {
                    category = new Category();

                    category.CategoryId = Db.ToInteger(row["CategoryId"]);
                    category.CategoryName = Db.ToString(row["CategoryName"]);
                    category.Description = Db.ToString(row["Description"]);
                    category.IsActive = Db.ToBoolean(row["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return category;
        }

        public int DeleteCategoryById(int categoryId, int staffId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[2];

                param[0] = new DbParam("@CategoryId", categoryId, SqlDbType.Int);
                param[1] = new DbParam("@StaffId", staffId, SqlDbType.Int);

                result = Db.Insert("SP_CATEGORY_DELETE_BY_ID", param, false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }

        public IList<Category> GetAllActiveCategories()
        {
            IList<Category> categories = null;

            try
            {
                DataTable dt = Db.GetDataTable("SP_CATEGORY_GET_ACTIVE", null);

                if (dt != null && dt.Rows.Count > 0)
                {
                    categories = new List<Category>();
                    foreach (DataRow row in dt.Rows)
                    {
                        Category category = new Category();
                        category.CategoryId = Db.ToInteger(row["CategoryId"]);
                        category.CategoryName = Db.ToString(row["CategoryName"]);
                        categories.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return categories;
        }

    }
}
