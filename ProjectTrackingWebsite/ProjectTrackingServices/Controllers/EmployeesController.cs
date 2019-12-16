using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ProjectTrackingServices.Models;


namespace ProjectTrackingServices.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        [Route("api/employees")]
        public HttpResponseMessage  Get()
        {
            var employees = EmployeesRepository.GetAllEmployees();
            HttpRequestMessage request = new HttpRequestMessage();
            var response = request.CreateResponse(HttpStatusCode.OK, employees);
            return response; 


        }

        [Route("api/employees/{id?}")]
        public HttpResponseMessage Get(int id)
        {
            var employees = EmployeesRepository.GetEmployeesByID(id);
            HttpRequestMessage request = new HttpRequestMessage();
            var response = request.CreateResponse(HttpStatusCode.OK, employees);
            return response;
        }

        [Route("api/employees/{name:alpha}")]
        public HttpResponseMessage Get(string name)
        {
            var employees = EmployeesRepository.GetEmployeeByName(name);
            HttpRequestMessage request = new HttpRequestMessage();
            var response = request.CreateResponse(HttpStatusCode.OK, employees);
            return response;
        }

        [Route("api/employees")]
        public HttpResponseMessage Post(Employee e)
        {
            var employees = EmployeesRepository.InsertEmployee(e);
            HttpRequestMessage request = new HttpRequestMessage();
            var response = request.CreateResponse(HttpStatusCode.OK, employees);
            return response;
        }

        [Route("api/employees")]
        public HttpResponseMessage Put(Employee e)
        {
            var employees = EmployeesRepository.UpdateEmployee(e);
            HttpRequestMessage request = new HttpRequestMessage();
            var response = request.CreateResponse(HttpStatusCode.OK, employees);
            return response;
        }

        [Route("api/employees")]
        public HttpResponseMessage Delete(Employee e)
        {
            var employees = EmployeesRepository.DeleteEmployee(e);
            HttpRequestMessage request = new HttpRequestMessage();
            var response = request.CreateResponse(HttpStatusCode.OK, employees);
            return response;
        }
    }
}
