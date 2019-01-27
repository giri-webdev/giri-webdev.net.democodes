using giri_webdev_livedemo.DbContexts;
using giri_webdev_livedemo.Models.CodeFirst;
using giri_webdev_livedemo.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace giri_webdev_livedemo.DAL
{
    public class PagingProcedureDAL
    {
        public ProductViewModel GetProducts(int pageIndex, int pageSize,string sortOrder,string sortColumn)
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                SqlParameter PageIndex = new SqlParameter("@PageIndex", pageIndex);
                SqlParameter PageSize = new SqlParameter("@PageSize", pageSize);
                SqlParameter RecordCount = new SqlParameter { ParameterName = "@RecordCount",Value=0, Direction = ParameterDirection.Output };
                SqlParameter SortOrder = new SqlParameter("@SortOrder",sortOrder.ToUpper());
                SqlParameter SortColumn = new SqlParameter("@SortColumn", sortColumn);

              List<Product> products=  context.Database.SqlQuery<Product>("Exec PagingProcedure @PageIndex,@PageSize,@RecordCount OUTPUT,@SortOrder,@SortColumn",
                    PageIndex, PageSize, RecordCount, SortOrder, SortColumn).ToList();

                ProductViewModel product = new ProductViewModel
                {
                    Products = products,
                    TotalRecordCount = (int)RecordCount.Value
                };

                return product;
            }
        }
    }
}