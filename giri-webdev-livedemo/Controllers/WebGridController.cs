using giri_webdev_livedemo.DAL;
using giri_webdev_livedemo.Models;
using giri_webdev_livedemo.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace giri_webdev_livedemo.Controllers
{
    /// <summary>
    /// JUNE-22-2015
    /// </summary>
    public class WebgridController : Controller
    {
        WebGridDAL dalobj = new WebGridDAL();
        //
        // GET: /Webgrid/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Webgridbs()
        {
            try
            {
                if (Session["webgridbs"] == null)
                {
                    Session["webgridbs"] = dalobj.GetEmployeesList();
                }
                return View(Session["webgridbs"] as List<EmployeeModel>);
            }
            catch (Exception exc)
            {
                ExceptionLogger.LogException(exc, "Method:Webgridbs Controller:Webgrid");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteEmployee(int Id)
        {
            try
            {
                List<EmployeeModel> lst = Session["webgridbs"] as List<EmployeeModel>;
                JqgridResult result = dalobj.DeleteEmployee(lst, Id);
                if (result != null)
                {
                    Session["webgridbs"] = result.EmpList;
                    return Json(new { result = "true" });
                }
            }
            catch (Exception exc)
            {
                ExceptionLogger.LogException(exc, "Method:DeleteEmployee Controller:Webgrid");
            }

            return Json(new { result = "false" });
        }

        [HttpPost]
        public ActionResult AddEmployee(string Name, string Age, string Salary)
        {
            try
            {
                List<EmployeeModel> lst = Session["webgridbs"] as List<EmployeeModel>;
                JqgridResult result = dalobj.AddEmployee(lst, Age, Name, Salary);
                if (result != null)
                {
                    Session["webgridbs"] = result.EmpList;
                    return Json(new { result = "true" });
                }
            }
            catch (Exception exc)
            {
                ExceptionLogger.LogException(exc, "Method:AddEmployee Controller:Webgrid");
            }
            return Json(new { result = "false" });
        }

        [HttpPost]
        public ActionResult UpdateEmployee(int Id, string Name, string Age, string Salary)
        {
            try
            {
                List<EmployeeModel> lst = Session["webgridbs"] as List<EmployeeModel>;
                JqgridResult result = dalobj.UpdateEmployee(lst, Id, Name, Age, Salary);
                if (result != null)
                {
                    Session["webgridbs"] = result.EmpList;
                    return Json(new { result = "true" });
                }
            }
            catch (Exception exc)
            {
                ExceptionLogger.LogException(exc, "Method:Webgridbs Controller:UpdateEmployee");
            }
            return Json(new { result = "false" });
        }


    }
}