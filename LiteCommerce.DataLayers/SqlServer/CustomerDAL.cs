using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
using System.Data.SqlClient;
using System.Data;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class CustomerDAL :ICustomerDAL
    {
        private string connectionString;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public CustomerDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Thêm vào 1 customer và hiển thị ad của customer vừa thêm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Customer data)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Đếm giá trị tìm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            //TODO : Sữa code đếm customers
            return 20;
        }
        /// <summary>
        /// Xóa nhiều customers,hiển thị số lượng đã xóa.
        /// </summary>
        /// <param name="customerIDs"></param>
        /// <returns></returns>
        public int Delete(int[] customerIDs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Lấy customer
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customer Get(int customerID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Hiển thị danh sách Customers
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Customer> List(int page, int pagesize, string searchValue)
        {
            List<Customer> data = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //Tạo lệnh thực thi truy vấn dữ liệu
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Customers";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Customer()
                        {
                            CustomerID = Convert.ToString(dbReader["CustomerID"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            CompanyName = Convert.ToString(dbReader["CompanyName"]),
                            ContactTitle = Convert.ToString(dbReader["ContactTitle"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"]),
                            Fax = Convert.ToString(dbReader["Fax"]),
                            
                        });
                    }
                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// Sửa customer và trả về bool
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Customer data)
        {
            throw new NotImplementedException();
        }
    }
}
