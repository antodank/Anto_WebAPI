using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;
using EmployeeWebAPIOne.Models;

namespace EmployeeWebAPIOne.Controllers
{
    public class EmployeeController : ApiController
    {
        //Web Api request mapped with method in a controller that has exact same name or starts with that particular HTTP verb 
        //HTTP GET request will map to get() mwthod if exist
        //HTTP GET request can also map to the GetEmployees() as it starts with valid HTTP verb

        [HttpGet]
        public HttpResponseMessage LoadAllEmployees()
        {
            try
            {
                EmployeeDBEntities entities = new EmployeeDBEntities();
                return Request.CreateResponse(HttpStatusCode.Found, entities.Employees.ToList());
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Data found");
            }
        }

        //HTTP GET doesn't support FromBody request it only supports URI request
        [HttpGet]
        public HttpResponseMessage LoadEmployeesByID(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                    Employee emp = entity as Employee;
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Employee with Id " + id.ToString() + " not found");
                }
            }
        }

        //[HttpGet]
        //public HttpResponseMessage LoadEmployeeByGender(string gender = "All")
        //{
        //    using (EmployeeDBEntities entities = new EmployeeDBEntities())
        //    {
        //        switch (gender.ToLower())
        //        {
        //            case "all":
        //                return Request.CreateResponse(HttpStatusCode.OK,
        //                    entities.Employees.ToList());
        //            case "male":
        //                return Request.CreateResponse(HttpStatusCode.OK,
        //                    entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList());
        //            case "female":
        //                return Request.CreateResponse(HttpStatusCode.OK,
        //                    entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());
        //            default:
        //                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
        //                    "Value for gender must be Male, Female or All. " + gender + " is invalid.");
        //        }
        //    }
        //}

        [HttpGet]
        public HttpResponseMessage LoadEmployeeByName(string fname, string lname)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {

                var employeeEntity = entities.Employees.Where(x => x.FirstName == fname && x.LastName == lname).ToList();

                if (employeeEntity?.Count > 0 )
                {
                    return Request.CreateResponse(HttpStatusCode.OK, employeeEntity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        "Employee name not found");
                }
            }
        }


        //Web API doesn't allow you to pass multiple complex objects in the method signature of a Web API controller method
        //you can post only a single value to a Web API action method. therfore following method is not supported.

        //[HttpPost]
        //[ActionName("Filter")]
        //public HttpResponseMessage FilterEmployee([FromBody] EmployeeSearchModel searchModel)
        //{
        //    using (EmployeeDBEntities entities = new EmployeeDBEntities())
        //    {
        //        var employeeEntity = entities.Employees.AsQueryable<Employee>();

        //        if (!string.IsNullOrEmpty(searchModel.FirstName) || !string.IsNullOrEmpty(searchModel.FirstName))
        //        {
        //            employeeEntity = employeeEntity.Where(x => x.FirstName.StartsWith(searchModel.FirstName) && x.LastName.StartsWith(searchModel.LastName));
        //        }
        //        else if (string.IsNullOrEmpty(searchModel.Gender))
        //        {
        //            employeeEntity = employeeEntity.Where(x => x.Gender == searchModel.Gender);
        //        }
        //        else if (searchModel.Salary.HasValue)
        //        {
        //            employeeEntity = employeeEntity.Where(x => x.Salary == searchModel.Salary);
        //        }



        //        if (employeeEntity != null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, employeeEntity);
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound,
        //                "No employee found.");
        //        }
        //    }
        //}


        [HttpPost]
        [ActionName("Add")]
        public HttpResponseMessage AddEmployee([FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();
                }

                var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                message.Headers.Location = new Uri(Request.RequestUri +
                    employee.ID.ToString());

                return message;
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //It is possible to pass multiple values though on a POST or a PUT operation by mapping one parameter to the actual content
        //and the remaining ones via query strings.
        //To pass mutiple complex objects you can create a single complex object that wraps the multiple parameters 

        [HttpPut]
        [ActionName("UpdateByID")]
        public HttpResponseMessage UpdateEmployee(int id, [FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id " + id.ToString() + " not found to update.");
                    }
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [ActionName("UpdateByName")]
        public HttpResponseMessage UpdateEmployee(string fullname, [FromBody]Employee employee)
        {
            try
            {
                string fname = string.Empty;
                string lname = string.Empty;
                if (fullname.Contains(' '))
                {
                    fname = fullname.Split(' ')[0];
                    lname = fullname.Split(' ')[1];
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee name not found.");
                }
                
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    
                    var entity = entities.Employees.FirstOrDefault(e => e.FirstName == fname && e.LastName == lname);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee name not found.");
                    }
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [HttpDelete]
        [ActionName("Remove")]
        public HttpResponseMessage RemoveEmployee(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Employee with Id = " + id.ToString() + " deleted.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



    }
}
