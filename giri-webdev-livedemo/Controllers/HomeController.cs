using giri_webdev_livedemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace giri_webdev_livedemo.Controllers
{
    /// <summary>
    /// JUNE-22-2015
    /// </summary>
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        #region ViewResultTypes
        public ActionResult ViewResultTypes()
        {
            return View();
        }

        public ActionResult ContentType()
        {
            try
            {
                //for xml "application/xml"
                // ContentResult
                return Content("Hello World!", "text/plain");
            }
            catch (Exception)
            { }
            return null;
        }

        public ActionResult GetFileData()
        {
            try
            {
                byte[] bytes = System.IO.File.
                ReadAllBytes(Server.MapPath(@"~/Images/profilepic1.jpg"));

                // FileResult
                return File(bytes, "image/jpeg");
            }
            catch (Exception)
            { }
            return null;
        }

        public ActionResult HttpError()
        {
            try
            {
                //HttpNotFoundResult
                return HttpNotFound("Custom error message. file not found");
            }
            catch (Exception)
            { }

            return null;
        }

        //example for returning view
        public ActionResult JsView()
        {
            //ViewResult
            return View();
        }

        public ActionResult GetJsCode()
        {
            try
            {
                //JavaScriptResult
                return JavaScript(@"alert('hello world')");
            }
            catch (Exception)
            { }

            return null;
        }

        public ActionResult GetJson()
        {
            try
            {
                List<Employee> lst = new List<Employee>();
                lst.Add(new Employee { Name = "Jim", Age = 20 });
                lst.Add(new Employee { Name = "Tom", Age = 30 });

                //JsonResult
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            { }

            return null;
        }

        //HTTP 302 STATUS CODE
        public ActionResult RedirectExample()
        {
            try
            {
                // RedirectResult
                return Redirect("http://msn.in");
            }
            catch (Exception)
            { }

            return null;
        }


        //HTTP 301 Status Code
        public ActionResult RedirectPermanentExample()
        {
            try
            {

                return RedirectPermanent("http://msn.in");
            }
            catch (Exception)
            { }

            return null;
        }

        //HTTP 302
        public ActionResult RedirectToActionTest()
        {
            try
            {

                return RedirectToAction("GetFileData");
            }
            catch (Exception)
            { }

            return null;
        }


        //HTTP 301
        public ActionResult RedirectToActionPmntTest()
        {
            try
            {
                return RedirectToActionPermanent("GetFileData");
            }
            catch (Exception)
            { }

            return null;
        }

        public ActionResult RedirectToRouteTest()
        {
            try
            {
                return RedirectToRoute(new RouteValueDictionary { { "controller", "Home" }, { "action", "ContentType" } });
            }
            catch (Exception)
            { }

            return null;
        }


        public ActionResult RedirectToRoutePermntTest()
        {
            try
            {
                return RedirectToRoutePermanent(new RouteValueDictionary { { "controller", "Home" }, { "action", "ContentType" } });
            }
            catch (Exception)
            { }

            return null;
        }

        public ActionResult ViewTest()
        {
            try
            {
                // return new ViewResult();
                return View();
            }
            catch (Exception)
            { }

            return null;
        }

        public ActionResult PartialViewTest()
        {
            try
            {
                return PartialView();
            }
            catch (Exception)
            { }

            return null;
        }


        public ActionResult EmptyTest()
        {
            try
            {
                return new EmptyResult();
            }
            catch (Exception)
            { }

            return null;
        }

        public ActionResult HttpUnauthTest()
        {
            try
            {
                return new HttpUnauthorizedResult();
            }
            catch (Exception)
            { }

            return null;
        }

        public ActionResult HttStatusCodeTest()
        {
            try
            {
                //bad request 
                return new HttpStatusCodeResult(400, "Custom Error Message");
            }
            catch (Exception)
            { }

            return null;
        }

        #endregion

        #region ModelBinder
        public ActionResult ModelBinder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ModelBinder([ModelBinder(typeof(EmployeeModelBinder))]EmployeesModel empobj)
        {
            ViewBag.Message = empobj.Name + " added successfully";
            return View();
        }
        #endregion

        #region CustomHelpers

        public ActionResult InlineHelper()
        {
            return View();
        }

        public ActionResult ExternalHelper()
        {
            return View();
        }
        #endregion
    }

    //ViewResultTypes
    public class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    //ModelBinder
    public class EmployeesModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}