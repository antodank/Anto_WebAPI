using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMongoBookAPI.DatabaseServices;
using CoreMongoBookAPI.Logger;
using CoreMongoBookAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreMongoBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly EmployeeService _employeeService;

        private readonly ILoggerManager _logger;

        public EmployeeController(ILoggerManager logger,EmployeeService employeeService)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            _logger.LogInfo("Here is info message from the controller.");
            _logger.LogDebug("Here is debug message from the controller.");
            _logger.LogWarn("Here is warn message from the controller.");
            _logger.LogError("Here is error message from the controller.");

            var employees = await _employeeService.Get();
            return Ok(employees);
        }

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public async Task<ActionResult<Employee>> Get(string id)
        {
            var employee = await _employeeService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // POST: api/Employee
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<Employee>> Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _employeeService.CreateAsync(employee);
             return CreatedAtRoute("GetUser", new { id = employee.Id.ToString() }, employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Put(string id, [FromBody] Employee updateEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var employee = await _employeeService.Get(id);
            if (employee == null)
            {
                return NotFound();
            }
            await _employeeService.UpdateAsync(id, updateEmployee);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            var employee = await _employeeService.Get(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
