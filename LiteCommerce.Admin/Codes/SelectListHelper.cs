using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin
{
    public class SelectListHelper
    {
        /// <summary>
        /// Các quốc gia 
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> List = new List<SelectListItem>();
            List.Add(new SelectListItem() { Value = "USA", Text = "United States" });
            List.Add(new SelectListItem() { Value = "UK", Text = "England" });
            List.Add(new SelectListItem() { Value = "VN", Text = "VietNam" });
            return List;
        }
    }
}