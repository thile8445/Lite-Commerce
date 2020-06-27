using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IAttributeDAL
    {
        List<DomainModels.Attribute> GetAll(int CategoryID);
        int Add(DomainModels.Attribute data);
        bool Delete(int AttributeID);
        bool Update(DomainModels.Attribute data);
    }
}
