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
    public class CategoryDAL : ICategoryDAL
    {
        private string connectionString;

        public CategoryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Category data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue)
        {
            return 20;
        }

        public int Delete(int[] categoryIDs)
        {
            throw new NotImplementedException();
        }

        public Category Get(int categoryID)
        {
            throw new NotImplementedException();
        }

        public List<Category> List(int page, int pagesize, string searchValue)
        {
            List<Category> data = new List<Category>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM CATEGORIES";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                using(SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        data.Add(new Category()
                        {
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            CategoryName = Convert.ToString(reader["CategoryName"]),
                            Description = Convert.ToString(reader["Description"])
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }

        public bool Update(Category data)
        {
            throw new NotImplementedException();
        }
    }
}
