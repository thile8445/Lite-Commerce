using LiteCommerce.DataLayers;
using LiteCommerce.DataLayers.SqlServer;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public static class CountryBLL
    {
        private static ICountryDAL CountryDB;

        public static void Initialize(string connectionString)
        {
            CountryDB = new CountryDAL(connectionString);
        }
        public static List<Country> GetList()
        {
            return CountryDB.GetAll();
        }
        public static int Delete(int[] countries)
        {
            return CountryDB.Delete(countries);
        }
        public static int Add(Country data)
        {
            return CountryDB.Add(data);
        }
        public static bool Update(Country data)
        {
            return CountryDB.Update(data);
        }
        public static List<Country> ListOfCountry(int page,int pageSize,string searchValue,out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = CountryDB.Count(searchValue);
            return CountryDB.List(page, pageSize, searchValue);
        }
        public static Country Get(int CountryID)
        {
            return CountryDB.Get(CountryID);
        }
    }
}
