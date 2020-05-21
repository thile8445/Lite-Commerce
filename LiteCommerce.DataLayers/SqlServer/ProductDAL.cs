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

        public int Count(string searchValue)
        {
            return 20;
        }

        public int Delete(int[] productIDs)
        {
            throw new NotImplementedException();
        }

        public Product Get(int productID)
        {
            throw new NotImplementedException();
        }

        public List<Product> List(int page, int pagesize, string searchValue)
        {
            List<Product> data = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Products";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
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
