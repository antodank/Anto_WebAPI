using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using EmployeeDataAccess;  

namespace EmployeeWebAPITwo.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class EmployeeTwoController : ApiController
    {
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

        //[HttpGet]
        //public HttpResponseMessage LoadEmployeesByID(int id)
        //{
        //    using (EmployeeDBEntities entities = new EmployeeDBEntities())
        //    {
        //        var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

        //        if (entity != null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, entity);
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound,
        //                "Employee with Id " + id.ToString() + " not found");
        //        }
        //    }
        //}

        ///api/employeetwo?Gender=all
        ///
        [HttpGet]
        public HttpResponseMessage LoadEmployeeByGender(string gender)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                            "Value for gender must be Male, Female or All. " + gender + " is invalid.");
                }
            }
        }

        //we can specify RequireHttpsAttribute here
        [HttpPost]
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
    }
}
