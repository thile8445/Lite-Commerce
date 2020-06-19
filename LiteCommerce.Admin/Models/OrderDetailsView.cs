using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class OrderDetailsView
    {
        public EntityOrder Order { get; set; }
        public List<OrderDetails> OrderDetails { get; set;}
    }
}