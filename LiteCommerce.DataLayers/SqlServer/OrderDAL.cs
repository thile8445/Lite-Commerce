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
            int orderID = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Orders
                                          (
                                              CustomerID,
                                              ShipperID,
                                              EmployeeID,
	                                          OrderDate,
	                                          RequiredDate,
	                                          ShippedDate,
	                                          Freight,
	                                          ShipAddress,
	                                          ShipCity,
                                              ShipCountry
                                          )
                                          VALUES
                                          (
                                              @CustomerID,
                                              @ShipperID,
                                              @EmployeeID,
	                                          @OrderDate,
	                                          @RequiredDate,
	                                          @ShippedDate,
	                                          @Freight,
	                                          @ShipAddress,
	                                          @ShipCity,
                                              @ShipCountry
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@OrderDate", data.OrderDate);
                cmd.Parameters.AddWithValue("@RequiredDate", data.RequiredDate);
                cmd.Parameters.AddWithValue("@ShippedDate", data.ShippedDate);
                cmd.Parameters.AddWithValue("@Freight", data.Freight);
                cmd.Parameters.AddWithValue("@ShipAddress", data.ShipAddress);
                cmd.Parameters.AddWithValue("@ShipCity", data.ShipCity);
                cmd.Parameters.AddWithValue("@ShipCountry", data.ShipCountry);
                orderID = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return orderID;
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
                cmd.CommandText = @"SELECT  count(*)
						FROM            Customers INNER JOIN
                         Orders ON Customers.CustomerID = Orders.CustomerID INNER JOIN
                         Employees ON Orders.EmployeeID = Employees.EmployeeID INNER JOIN
                         Shippers ON Orders.ShipperID = Shippers.ShipperID
	                        where  ((@searchValue =N'') or (Customers.CompanyName like @searchValue) or (Shippers.CompanyName like @searchValue)) and ((@Country =N'') or (ShipCountry like @Country))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@Country", country);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return count;
        }

        public int CountAll()
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT  count(*)
						FROM   Orders";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                count = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return count;
        }

        public string CustomerNameToID(string customerName)
        {
            string CustomerID = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select CustomerID from Customers where CompanyName = @CompanyName ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CompanyName", customerName);
                CustomerID = Convert.ToString(cmd.ExecuteScalar());

                connection.Close();
            }
            return CustomerID;
        }

        public int Delete(int[] orderIDs)
        {
            throw new NotImplementedException();
        }

        public int EmployeeNametoID(string employeeName)
        {
            int EmployeeID = 0;
            string[] Name = employeeName.Split(' ');
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select EmployeeID from Employees where LastName =@LastName and FirstName=@FirstName";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@LastName",Name[0]);
                cmd.Parameters.AddWithValue("@FirstName",Name[1]);
                EmployeeID = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return EmployeeID;
        }

        public EntityOrder Get(int orderID)
        {
            EntityOrder data = new EntityOrder();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = @"SELECT  Orders.OrderID,Customers.CompanyName as CustomerCompanyName, 
                             Shippers.CompanyName AS ShipperCompanyName, Orders.OrderDate, Orders.RequiredDate, 
                             Orders.ShippedDate, Orders.Freight,
                             Orders.ShipAddress, Orders.ShipCity, Orders.ShipCountry, Employees.LastName, 
                             Employees.FirstName
						     FROM  Customers INNER JOIN
                             Orders ON Customers.CustomerID = Orders.CustomerID INNER JOIN
                             Employees ON Orders.EmployeeID = Employees.EmployeeID INNER JOIN
                             Shippers ON Orders.ShipperID = Shippers.ShipperID
						     where OrderID = @OrderID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        data = new EntityOrder()
                        {
                            OrderID = Convert.ToInt32(reader["OrderID"]),
                            CustomerCompanyName = Convert.ToString(reader["CustomerCompanyName"]),
                            FullName = Convert.ToString(reader["FirstName"]) + " " + Convert.ToString(reader["LastName"]),
                            Freight = Convert.ToDecimal(reader["Freight"]),
                            OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                            RequiredDate = Convert.ToDateTime(reader["RequiredDate"]),
                            ShippedDate = Convert.ToDateTime(reader["ShippedDate"]),
                            ShipAddress = Convert.ToString(reader["ShipAddress"]),
                            ShipCity = Convert.ToString(reader["ShipCity"]),
                            ShipCountry = Convert.ToString(reader["ShipCountry"]),
                            ShipperCompanyName = Convert.ToString(reader["ShipperCompanyName"])
                        };
                    }
                }

                connection.Close();
            }

            return data;
        }

        public List<OrderDetails> GetAll(int orderID)
        {
            List<OrderDetails> data = new List<OrderDetails>();
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT OrderDetails.*, Products.ProductName
                                        FROM   OrderDetails INNER JOIN
                                        Products ON OrderDetails.ProductID = Products.ProductID
                                        where OrderID = @OrderID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        data.Add(new OrderDetails()
                        {
                            OrderID = Convert.ToInt32(reader["OrderID"]),
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            UnitPrice = Convert.ToDouble(reader["UnitPrice"]),
                            Discount = Convert.ToDouble(reader["Discount"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            ProductName = Convert.ToString(reader["ProductName"])
                        });
                    }
                }
                connection.Close();
            }

            return data;
        }

        public List<EntityOrder> List(int page, int pagesize, string searchValue,string country)
        {
            List<EntityOrder> data = new List<EntityOrder>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * 
                        from 
                        (
	                        SELECT row_number() over(order by Customers.CompanyName) as RowNumber, Orders.OrderID,Customers.CompanyName as CustomerCompanyName, Shippers.CompanyName AS ShipperCompanyName, Orders.OrderDate, Orders.RequiredDate, Orders.ShippedDate, Orders.Freight, Orders.ShipAddress, Orders.ShipCity, Orders.ShipCountry, Employees.LastName, 
                         Employees.FirstName
						FROM            Customers INNER JOIN
                         Orders ON Customers.CustomerID = Orders.CustomerID INNER JOIN
                         Employees ON Orders.EmployeeID = Employees.EmployeeID INNER JOIN
                         Shippers ON Orders.ShipperID = Shippers.ShipperID
	                        where  ((@searchValue =N'') or (Customers.CompanyName like @searchValue) or (Shippers.CompanyName like @searchValue)) and ((@Country =N'') or (ShipCountry like @Country))
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
                        data.Add(new EntityOrder()
                        {
                            OrderID = Convert.ToInt32(reader["OrderID"]),
                            CustomerCompanyName = Convert.ToString(reader["CustomerCompanyName"]),
                            FullName = Convert.ToString(reader["FirstName"])+" "+ Convert.ToString(reader["LastName"]),
                            Freight = Convert.ToDecimal(reader["Freight"]),
                            OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                            RequiredDate = Convert.ToDateTime(reader["RequiredDate"]),
                            ShippedDate = Convert.ToDateTime(reader["ShippedDate"]),
                            ShipAddress = Convert.ToString(reader["ShipAddress"]),
                            ShipCity = Convert.ToString(reader["ShipCity"]),
                            ShipCountry = Convert.ToString(reader["ShipCountry"]),
                            ShipperCompanyName = Convert.ToString(reader["ShipperCompanyName"])                       
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }

        public int ShipperNametoID(string ShipperName)
        {
            int ShipperID = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select ShipperID from Shippers where CompanyName = @CompanyName ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CompanyName",ShipperName);
                ShipperID = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return ShipperID;
        }

        public double Total(List<OrderDetails> list)
        {
            double total = 0;
            foreach (var amount in list)
            {
                total += amount.Amount;
            }
            return total;
        }

        public bool Update(Order data)
        {
            throw new NotImplementedException();
        }
    }
}
