using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models.Dashboard
{
    public class DashboardResult
    {
        public int countEmployees { get; set; }
        public int countOrders { get; set; }
        public int countSuppliers { get; set; }
        public int countShippers { get; set; }
        public int countCustomers { get; set; }
        public int countCategories { get; set; }
        public int countProducts{ get; set; }
    }
}