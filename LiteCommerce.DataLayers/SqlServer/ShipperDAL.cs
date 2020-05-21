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
    public class ShipperDAL : IShipperDAL
    {
        private string connectionString;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public ShipperDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Shipper data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue)
        {
            return 20;
        }

        public int Delete(int[] shipperIDs)
        {
            throw new NotImplementedException();
        }

        public Shipper Get(int shipperID)
        {
            throw new NotImplementedException();
        }

        public List<Shipper> List(int page, int pagesize, string searchValue)
        {
            List<Shipper> data = new List<Shipper>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM SHIPPERS";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Shipper()
                        {
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            CompanyName = Convert.ToString(dbReader["CompanyName"]),
                            Phone = Convert.ToString(dbReader["Phone"])
                        });
                    }
                }
                connection.Close();
            }

            return data;
        }

        public bool Update(Shipper data)
        {
            throw new NotImplementedException();
        }
    }
}
