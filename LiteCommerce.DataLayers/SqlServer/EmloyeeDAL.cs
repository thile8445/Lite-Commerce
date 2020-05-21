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
            return 20;
        }

        public int Delete(int[] employeeIDs)
        {
            throw new NotImplementedException();
        }

        public Employee Get(int employeeID)
        {
            throw new NotImplementedException();
        }

        public List<Employee> List(int page, int pagesize, string searchValue)
        {
            List<Employee> data = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Employees";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
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
