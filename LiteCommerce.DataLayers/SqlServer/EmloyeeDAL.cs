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
    public class EmloyeeDAL :IEmployeeDAL
    {
        private string connectionString;
        public EmloyeeDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Employee data)
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
                cmd.CommandText = @"select count(*) as count from Employees 
                        where @searchValue = N'' or FirstName like @searchValue or LastName like @searchValue";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return count;
        }

        public int Delete(int[] employeeIDs)
        {
            throw new NotImplementedException();
        }

        public Employee Get(int employeeID)
        {
            throw new NotImplementedException();
        }

        public List<Employee> List(int page, int pageSize, string searchValue)
        {
            List<Employee> data = new List<Employee>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * 
                                from
                                (
                                    select row_number() over(order by FirstName) as RowNumber,
                                            Employees.*
                                    from Employees

                                    where (@searchValue = N'') or(LastName like @searchValue) or(FirstName like @searchValue)
                                ) as t
                                where t.RowNumber between  (@page - 1) * @pageSize + 1 and @page * @pageSize
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
                        data.Add(new Employee()
                        {
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            FirstName = Convert.ToString(reader["FirstName"]),
                            Title = Convert.ToString(reader["Title"]),
                            BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                            HireDate = Convert.ToDateTime(reader["HireDate"]),
                            Email = Convert.ToString(reader["Email"]),
                            Address = Convert.ToString(reader["Address"]),
                            City = Convert.ToString(reader["City"]),
                            Country = Convert.ToString(reader["Country"]),
                            HomePhone = Convert.ToString(reader["HomePhone"]),
                            PhotoPath = Convert.ToString(reader["PhotoPath"]),
                            Notes = Convert.ToString(reader["Notes"]),
                            Password = Convert.ToString(reader["Password"])
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }

        public bool Update(Employee data)
        {
            throw new NotImplementedException();
        }
    }
}
