using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models.Attribute
{
    public class ProductAttributeModel :Attribute
    {
        public string AttributeValues { get; set; }
        public int DisplayOrder { get; set; }
        public int ProductID { get; set; }
        public int ProductAttributeID { get; set; }
    }
}