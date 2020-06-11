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
    public class OrderDAL :IOrderDAL
    {
        private string connectionString;
        public OrderDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Order data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue)
        {
            return 20;
        }

        public int Delete(int[] orderIDs)
        {
            throw new NotImplementedException();
        }

        public Order Get(int orderID)
        {
            throw new NotImplementedException();
        }

        public List<Order> List(int page, int pagesize, string searchValue)
        {
            List<Order> data = new List<Order>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Orders";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        data.Add(new Order()
                        {
                            OrderID = Convert.ToInt32(reader["OrderID"]),
                            CustomerID = Convert.ToString(reader["CustomerID"]),
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            Freight = Convert.ToDecimal(reader["Freight"]),
                            OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                            RequiredDate = Convert.ToDateTime(reader["RequiredDate"]),
                            ShippedDate = Convert.ToDateTime(reader["ShippedDate"]),
                            ShipAddress = Convert.ToString(reader["ShipAddress"]),
                            ShipCity = Convert.ToString(reader["ShipCity"]),
                            ShipCountry = Convert.ToString(reader["ShipCountry"]),
                            ShipperID = Convert.ToInt32(reader["ShipperID"])                       
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }

        public bool Update(Order data)
        {
            throw new NotImplementedException();
        }
    }
}
