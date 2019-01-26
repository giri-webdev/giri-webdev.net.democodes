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
    public class JqGridController : Controller
    {
        JqGridDAL dalobj = new JqGridDAL();

        public ActionResult Index()
        {
            return View();
        }

        #region CRUD Operations
        [HttpPost]
        public string GetEmployees(string _searchTerm = "",
            bool _search = false, string searchField = "", string searchString = "", string searchOper = "",
            int rows = 10, int page = 1, string sidx = null, string sord = "asc")
        {
            try
            {
                if (Session["empList"] == null)
                    Session["empList"] = dalobj.GetEmployeesList();
                List<EmployeeModel> empList = (List<EmployeeModel>)Session["empList"];
                return dalobj.GetEmployees(empList, _searchTerm, _search, searchField, searchString, searchOper,
                    rows, page, sidx, sord);
            }
            catch (Exception exc)
            {
                ExceptionLogger.LogException(exc, "Method: GetEmployees Controller: Jqgrid");
            }

            return null;
        }


        public string Update(string Age, int EmpId, string Name, string Salary, int id, string oper)
        {
            try
            {
                if (oper == "edit")
                {
                    List<EmployeeModel> empList = (List<EmployeeModel>)Session["empList"];
                    JqgridResult result = dalobj.UpdateEmployee(empList,
                        Age, EmpId, Name, Salary, id, oper);
                    if (result != null)
                    {
                        Session["empList"] = result.EmpList;
                        return result.Result;
                    }
                }
                return "Update failed";
            }
            catch (Exception exc)
            {
                ExceptionLogger.LogException(exc, "Method: Update Controller: Jqgrid");
            }
            return "Update failed.";
        }

        public string Add(int Age, string Name, string Salary, string id, string oper)
        {
            try
            {
                if (oper == "add")
                {
                    List<EmployeeModel> empList = (List<EmployeeModel>)Session["empList"];
                    JqgridResult result = dalobj.AddEmployee(empList, Age,
                        Name, Salary, id, oper);
                    if (result != null)
                    {
                        Session["empList"] = result.EmpList;
                        return result.Result;
                    }
                }
                return "Add operation failed";
            }
            catch (Exception exc)
            {
                ExceptionLogger.LogException(exc, "Method: Add Controller: Jqgrid");
            }
            return "Add operation failed.";
        }

        public string Delete(int ID, int id, string oper)
        {
            try
            {
                if (oper == "del")
                {
                    List<EmployeeModel> empList = (List<EmployeeModel>)Session["empList"];
                    JqgridResult result = dalobj.DeleteEmployee(empList, ID, id, oper);
                    if (result != null)
                    {
                        Session["empList"] = result.EmpList;
                        return result.Result;
                    }
                }
                return "Delete operation failed";
            }
            catch (Exception exc)
            {
                ExceptionLogger.LogException(exc, "Method: Delete Controller: Jqgrid");
            }
            return "Delete operation failed.";

        }

        #endregion

        [HttpGet]
        public string FavoriteTheme(string Theme)
        {
            Session["FavTheme"] = Theme;

            return "Success";
        }
    }
}