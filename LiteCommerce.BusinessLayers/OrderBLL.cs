using LiteCommerce.DataLayers;
using LiteCommerce.DataLayers.SqlServer;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public static class OrderBLL
    {
        /// <summary>
        ///  Hàm phải được gọi để khởi tạo chức năng tác nghiệp
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            OrderDB = new OrderDAL(connectionString);
        }
        #region Khai báo các thuộc tính giao tiếp với DAL

        private static IOrderDAL OrderDB { get; set; }

        #endregion

        #region Khai báo các chức năng xử lý nghiệp vụ
        /// <summary>
        /// Hiển thị danh sách của orders
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Order> ListOfOrders(int page, int pageSize, string searchValue,string country ,out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = OrderDB.Count(searchValue,country);
            return OrderDB.List(page, pageSize, searchValue,country);
        }
        #endregion
    }
}
