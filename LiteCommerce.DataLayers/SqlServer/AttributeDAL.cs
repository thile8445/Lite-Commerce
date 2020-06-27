using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
namespace LiteCommerce.DataLayers.SqlServer
{
    public class AttributeDAL : IAttributeDAL
    {
        private string connectionString;
        public AttributeDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(DomainModels.Attribute data)
        {
            int attributeID = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Attributes
                                          (
	                                          AttributeName,CategoryID
                                          )
                                          VALUES
                                          (
	                                          @AttributeName,@CategoryID
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                attributeID = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return attributeID;
        }

        public bool Delete(int AttributeID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Attributes
                                            WHERE AttributeID = @AttributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@AttributeID", AttributeID);
                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
                connection.Close();
            }

            return rowsAffected > 0;
        }

        public bool Update(DomainModels.Attribute data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Attributes
                                           SET AttributeName = @AttributeName     
                                          WHERE AttributeID = @AttributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);
                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0;
        }

        List<DomainModels.Attribute> IAttributeDAL.GetAll(int CategoryID)
        {
            List<DomainModels.Attribute> data = new List<DomainModels.Attribute>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from Attributes where CategoryID = @categoryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@categoryID", CategoryID);
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        data.Add(new DomainModels.Attribute()
                        {
                            AttributeID = Convert.ToInt32(reader["AttributeID"]),
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            AttributeName = Convert.ToString(reader["AttributeName"])

                        });
                    }
                }
                connection.Close();

            }
            return data;
        }

        
    }
}
