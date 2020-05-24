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
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select count(*) as count from Categories 
                        where @searchValue = N'' or CategoryName like @searchValue";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return count;
        }

        public int Delete(int[] categoryIDs)
        {
            throw new NotImplementedException();
        }

        public Category Get(int categoryID)
        {
            throw new NotImplementedException();
        }

        public List<Category> List(int page, int pageSize, string searchValue)
        {
            List<Category> data = new List<Category>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * 
                                from
                                (
	                                select row_number() over(order by CategoryName) as RowNumber,
			                                Categories.*
	                                from Categories
	                                where (@searchValue =N'') or (CategoryName like @searchValue)
                                ) as t
                                where t.RowNumber between  (@page-1)*@pageSize + 1 and @page*@pageSize
                                order by t.RowNumber";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
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
