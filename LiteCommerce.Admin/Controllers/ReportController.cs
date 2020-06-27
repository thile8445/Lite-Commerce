using LiteCommerce.Admin.Models.Report;
using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index(string type ="")
        {
            ReportResult listReport = new ReportResult();
            List<Employee> listEmployee = EmloyeeBLL.GetAllEmployee();
            //if (type.Equals("staff"))
            //{
            //    foreach(var role in listEmployee)
            //    {
            //        if (role.Roles.Contains(type))
            //        {
            //            countStaff++;
            //            //listReport.listEmployee.Add(new Employee()
            //            //{
            //            //    LastName = role.LastName,
            //            //    FirstName = role.FirstName,
            //            //    Roles = role.Roles
            //            //});
            //        }
            //    }
            //}
            //else if (type.Equals("manageaccount"))
            //{
            //    foreach (var role in listEmployee)
            //    {
            //        if (role.Roles.Contains(type))
            //        {
            //            countManageAccount++;
            //            //listReport.listEmployee.Add(new Employee()
            //            //{
            //            //    LastName = role.LastName,
            //            //    FirstName = role.FirstName,
            //            //    Roles = role.Roles
            //            //});
            //        }
            //    }
            //}
            //else if (type.Equals("managedata"))
            //{
            //    foreach (var role in listEmployee)
            //    {
            //        countManageData++;
            //        //if (role.Roles.Contains(type))
            //        //{
            //        //    listReport.listEmployee.Add(new Employee()
            //        //    {
            //        //        LastName = role.LastName,
            //        //        FirstName = role.FirstName,
            //        //        Roles = role.Roles
            //        //    });
            //        //}
            //    }
            //}
            listReport.listEmployee = listEmployee;
            ViewBag.countStaff = EmloyeeBLL.CountRoles(listEmployee,"staff");
            ViewBag.countManageAccount = EmloyeeBLL.CountRoles(listEmployee,"manageaccount");
            ViewBag.countManageData = EmloyeeBLL.CountRoles(listEmployee,"managedata");
            return View(listReport);
        }
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create new Report";
            }
            else
            {
                ViewBag.Title = "Edit a Report";
            }
            return View();
        }
    }
}