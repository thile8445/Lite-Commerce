using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class OrderPaginationResult : PaginationResult
    {
        public List<Order> Data { get; set; }
        public string ShipperCountry { get; set; }
    }
}