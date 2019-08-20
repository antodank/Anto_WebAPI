using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;



namespace EmployeeWebAPIFive.Controllers
{
    public class EmployeeFiveController : ApiController
    {
        [HttpGet, Authorize]
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

        
        [HttpGet, Authorize]
        public HttpResponseMessage LoadEmployeesByID(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Employee with Id " + id.ToString() + " not found");
                }
            }
        }
    }
}