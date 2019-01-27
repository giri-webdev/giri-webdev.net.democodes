using giri_webdev_livedemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace giri_webdev_livedemo.DAL
{
    /// <summary>
    /// JUNE-22-2015
    /// </summary>
    public class WebGridDAL
    {
        #region CRUD
        public JqgridResult UpdateEmployee(List<EmployeeModel> empList, int Id,
            string Name, string Age, string Salary
          )
        {
            #region Validation
            string data = "";
            int age;
            decimal salary;
            bool result;

            //age validation
            result = int.TryParse(Age, out age);
            if (!result)
                data += "Please type the valid age.";

            //name validation
            if (Name.Length == 0)
                data += "," + "Please type the name.";

            //salary validation
            result = decimal.TryParse(Salary, out salary);
            if (!result)
                data += "," + "Please type the valid salary.";

            #endregion

            //update employee record
            if (string.IsNullOrEmpty(data))
            {
                EmployeeModel obj = (from emp in empList
                                     where emp.ID == Id
                                     select emp).FirstOrDefault();
                EmployeeModel updateObj = obj;
                empList.Remove(obj);

                //update
                updateObj.Name = Name;
                updateObj.Age = age;
                updateObj.Salary = salary;

                empList.Add(updateObj);

                JqgridResult updateResult = new JqgridResult();
                updateResult.EmpList = empList;
                updateResult.Result = "Employee updated successfully.";

                return updateResult;
            }
            return null;
        }

        public JqgridResult AddEmployee(List<EmployeeModel> empList,
            string Age, string Name, string Salary)
        {
            #region Validation
            string data = "";

            int age;
            decimal salary;
            bool result;

            //age validation
            result = int.TryParse(Age, out age);
            if (!result)
                data += "Please type the valid age.";

            //name validation
            if (Name.Length == 0)
                data += "," + "Please type the name.";

            //salary validation
            result = decimal.TryParse(Salary, out salary);
            if (!result)
                data += "," + "Please type the valid salary.";

            #endregion

            //Add new employee
            if (string.IsNullOrEmpty(data))
            {
                EmployeeModel empobj = empList.OrderByDescending(emp => emp.ID).First();
                EmployeeModel obj = new EmployeeModel()
                {
                    ID = (empobj.ID + 1),
                    Name = Name,
                    Age = age,
                    Salary = salary
                };

                empList.Add(obj);

                JqgridResult addResult = new JqgridResult();
                addResult.EmpList = empList;
                addResult.Result = "Employee added successfully";

                return addResult;
            }

            return null;
        }

        public JqgridResult DeleteEmployee(List<EmployeeModel> empList, int Id)
        {
            var obj = empList.Single(emp => emp.ID == Id);
            empList.Remove(obj);
            JqgridResult result = new JqgridResult();
            result.EmpList = empList;
            result.Result = "Employee deleted successfully";

            return result;
        }

        #endregion

        public List<EmployeeModel> GetEmployeesList()
        {
            #region EmployeesList
            List<EmployeeModel> lst = new List<EmployeeModel>();
            Random rndage = new Random();
            Random rndsalary = new Random();
            string[] names = { 
                             "Giri Prasad","Yathish","Rahul","Bala","Gowri","Jim",
                             "Aditya","Ajay","Ajit","Akash","Bhuvanesh","Tom","Watson",
                              "Emily","Amelia","Lilly","Rosy","Ruby","Jack","Harry","Jonathan",
                              "Daniel","Charlie","Jacob","Tony","William","Victor","Cindy","Bill",
                              "Alex"
                             };

            for (var i = 0; i < 30; i++)
            {

                lst.Add(new EmployeeModel { ID = (i + 1), Name = names[i], Age = rndage.Next(20, 100), Salary = rndsalary.Next(6000, 80000) });
            }
            return lst;
            #endregion
        }
    }
}