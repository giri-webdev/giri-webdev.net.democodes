using giri_webdev_livedemo.Controllers;
using System;
using System.Web.Mvc;

namespace giri_webdev_livedemo.Utilities
{
    /// <summary>
    /// JUNE-22-2015
    /// </summary>
    public class EmployeeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext cntrlrContext, ModelBindingContext bindingContext)
        {

            ValueProviderResult valFirstName = bindingContext.ValueProvider.GetValue("FirstName");
            ValueProviderResult valLastName = bindingContext.ValueProvider.GetValue("LastName");
            ValueProviderResult valAge = bindingContext.ValueProvider.GetValue("Age");

            string firstName = valFirstName.AttemptedValue ?? null;
            string lastName = valLastName.AttemptedValue ?? null;
            int Age = valAge.AttemptedValue != null ? Convert.ToInt32(valAge.AttemptedValue) : 0;

            EmployeesModel empModel = new EmployeesModel();
            empModel.FirstName = firstName;
            empModel.LastName = lastName;
            empModel.Name = firstName + " " + lastName;
            empModel.Age = Age;
            return empModel;
            // string Name = firstName+ " "+lastName;
            //ValueProviderResult val = new ValueProviderResult(Name,Name,valFirstName.Culture);
            //ModelStateDictionary mstate = bindingContext.ModelState;
            //mstate.SetModelValue("Name", val);
        }
    }
}