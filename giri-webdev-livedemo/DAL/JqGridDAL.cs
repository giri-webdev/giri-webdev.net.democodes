using giri_webdev_livedemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace giri_webdev_livedemo.DAL
{
    /// <summary>
    /// JUNE-22-2015
    /// </summary>
    public class JqGridDAL
    {
        #region CRUD
        public string GetEmployees(List<EmployeeModel> empList, string _searchTerm = "",
             bool _search = false, string searchField = "", string searchString = "", string searchOper = "",
             int rows = 10, int page = 1, string sidx = null, string sord = "asc")
        {

            IEnumerable<EmployeeModel> lst = empList;
            //searching
            #region Searching
            if (_search == true)
            {
                DataTable dt = giri_webdev_livedemo.Extensions.
                    DataTableExtensions.ConvertToDatatable<EmployeeModel>(empList);

                IEnumerable<DataRow> fltrdt = null;

                //equal
                if (searchOper == "eq")
                {
                    fltrdt = from row in dt.AsEnumerable()
                             where row.Field<int>(searchField) == Convert.ToInt32(searchString)
                             select row;
                }
                //not equal
                else if (searchOper == "ne")
                {
                    fltrdt = from row in dt.AsEnumerable()
                             where row.Field<int>(searchField) != Convert.ToInt32(searchString)
                             select row;
                }
                //contains
                else if (searchOper == "cn")
                {
                    fltrdt = from row in dt.AsEnumerable()
                             where row.Field<string>(searchField).ToLower().Contains(searchString.ToLower())
                             select row;
                }
                //begins with
                else if (searchOper == "bw")
                {
                    fltrdt = from row in dt.AsEnumerable()
                             where row.Field<string>(searchField).ToLower().StartsWith(searchString.ToLower())
                             select row;
                }
                //ends with
                else if (searchOper == "ew")
                {
                    fltrdt = from row in dt.AsEnumerable()
                             where row.Field<string>(searchField).ToLower().EndsWith(searchString.ToLower())
                             select row;
                }

                empList = new List<EmployeeModel>();

                foreach (DataRow row in fltrdt.ToList())
                {
                    empList.Add(new EmployeeModel
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Name = (string)row["Name"],
                        Age = (int)row["Age"],
                        Salary = (decimal)row["Salary"]
                    });
                }

                return JsonConvert.SerializeObject(empList);
            }
            #endregion

            //sorting
            #region sorting

            if (sidx == "ID")
            {
                if (sord == "desc")
                    lst = empList.OrderByDescending(emp => emp.ID);
                else if (sord == "asc")
                    lst = empList.OrderBy(emp => emp.ID);
            }
            else if (sidx == "Name")
            {
                if (sord == "desc")
                    lst = empList.OrderByDescending(emp => emp.Name);
                else if (sord == "asc")
                    lst = empList.OrderBy(emp => emp.Name);
            }
            else if (sidx == "Age")
            {
                if (sord == "desc")
                    lst = empList.OrderByDescending(emp => emp.Age);
                else if (sord == "asc")
                    lst = empList.OrderBy(emp => emp.Age);
            }
            else if (sidx == "Salary")
            {
                if (sord == "desc")
                    lst = empList.OrderByDescending(emp => emp.Salary);
                else if (sord == "asc")
                    lst = empList.OrderBy(emp => emp.Salary);
            }

            #endregion

            //paging
            lst = lst.Skip((page - 1) * rows).Take(rows);

            return JsonConvert.SerializeObject(lst);
        }

        public JqgridResult UpdateEmployee(List<EmployeeModel> empList, string Age,
            int EmpId, string Name, string Salary, int id, string oper)
        {
            #region Validation
            string data = "";


            int age;
            decimal salary;
            bool result;

            //age validation
            result = int.TryParse(Age.ToString(), out age);
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
                                     where emp.ID == EmpId
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
            int Age, string Name, string Salary, string id, string oper)
        {
            #region Validation
            string data = "";

            int age;
            decimal salary;
            bool result;

            //age validation
            result = int.TryParse(Age.ToString(), out age);
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

        public JqgridResult DeleteEmployee(List<EmployeeModel> empList, int ID, int id, string oper)
        {
            var obj = empList.Single(emp => emp.ID == ID);
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