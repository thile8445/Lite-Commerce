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

        public int Count(string searchValue,string country)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select count(*) as count from Orders 
                     where  ((@searchValue =N'') or (ShipAddress like @searchValue)) and ((@Country =N'') or (ShipCountry like @Country))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@Country", country);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return count;
        }

        public int Delete(int[] orderIDs)
        {
            throw new NotImplementedException();
        }

        public Order Get(int orderID)
        {
            throw new NotImplementedException();
        }

        public List<Order> List(int page, int pagesize, string searchValue,string country)
        {
            List<Order> data = new List<Order>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * 
                        from 
                        (
	                        select row_number() over(order by ShipAddress) as RowNumber,
			                        Orders.*
	                    from Orders 
	                        where  ((@searchValue =N'') or (ShipAddress like @searchValue)) and ((@Country =N'') or (ShipCountry like @Country))
                        ) as t
                        where   t.RowNumber between  (@page-1)*@pageSize + 1 and @page*@pageSize
                        order by t.RowNumber";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pagesize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@Country", country);
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
