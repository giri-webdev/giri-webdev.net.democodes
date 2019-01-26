using giri_webdev_livedemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace giri_webdev_livedemo.Controllers
{
    /// <summary>
    /// JUNE-22-2015
    /// </summary>
    public class MvcExamplesController : Controller
    {
        #region Validation
        public ActionResult ServerValidation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ServerValidation(ValidationModel employeeModel)
        {
            //Validate user input
            if (ModelState.IsValid)
            {
                //Implement db logic to save the new employee

                //Return successful message
                TempData["Message"] = "Employee added successfully.";

                //To avoid the form postback, on refresh of the browser.
                //Redirect to the get method
                return RedirectToAction("Index");
            }
            return View(employeeModel);
        }

        public ActionResult ClientValidation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClientValidation(ValidationModel employeeModel)
        {
            //Validate user input
            if (ModelState.IsValid)
            {
                //Implement db logic to save the new employee

                //Return successful message
                TempData["Message"] = "Employee added successfully.";

                //To avoid the form postback, on refresh of the browser.
                //Redirect to the get method
                return RedirectToAction("Index");
            }
            return View(employeeModel);
        }
        #endregion
    }
}