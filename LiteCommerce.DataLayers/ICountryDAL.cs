using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ICountryDAL
    {
        List<Country> GetAll();
        int Delete(int[] countries);
        bool Update(Country data);
        int Add(Country data);
        int Count(string searchValue);
        List<Country> List(int page, int pagesize, string searchValue);
        Country Get(int countryID);
    }
}
