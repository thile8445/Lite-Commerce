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
    public class ProductDAL : IProductDAL
    {
        private string connectionString;
        public ProductDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Product data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue,string Category,string Supplier)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select count(*) as count from Products 
                    where ((@searchValue =N'') or (ProductName like @searchValue)) and (((@Category =N'') or (CategoryID like @Category)) and ((@Supplier =N'') or (SupplierID like @Supplier)) and (ProductName like @searchValue))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@Supplier", Supplier);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return count;
        }

        public int Delete(int[] productIDs)
        {
            throw new NotImplementedException();
        }

        public Product Get(int productID)
        {
            throw new NotImplementedException();
        }

        public List<Product> List(int page, int pageSize, string searchValue,string Category,string Supplier)
        {
            List<Product> data = new List<Product>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * 
                        from
                        (
	                        select row_number() over(order by ProductName) as RowNumber,
			                        Products.*
	                        from Products
	                        where ((@searchValue =N'') or (ProductName like @searchValue)) and ((@Category =N'') or (CategoryID like @Category) and (@Supplier =N'') or (SupplierID like @Supplier))
                        ) as t
                        where t.RowNumber between  (@page-1)*@pageSize + 1 and @page*@pageSize
                        order by t.RowNumber";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@Supplier", Supplier);
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        data.Add(new Product()
                        {
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            ProductName = Convert.ToString(reader["ProductName"]),
                            SupplierID = Convert.ToInt32(reader["SupplierID"]),
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            QuantityPerUnit = Convert.ToString(reader["QuantityPerUnit"]),
                            UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                            PhotoPath = Convert.ToString(reader["PhotoPath"]),
                            Descriptions = Convert.ToString(reader["Descriptions"])
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }

        public bool Update(Product data)
        {
            throw new NotImplementedException();
        }
    }
}
