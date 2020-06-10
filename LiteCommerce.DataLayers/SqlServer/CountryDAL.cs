using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class CountryDAL:ICountryDAL
    {
        private string connectionString;
        public CountryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<Country> getAll()
        {
            List<Country> data = new List<Country>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Select * from Countries";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection) )
                {
                    while (reader.Read())
                    {
                        data.Add(new Country
                        {
                            CountryID = Convert.ToInt32(reader["CountryID"]),
                            CountryName = Convert.ToString(reader["CountryName"]),
                            Abbreviation = Convert.ToString(reader["Abbreviation"])
                        });
                    }
                }
                connection.Close();
            }

            return data;
        }
    }
}
