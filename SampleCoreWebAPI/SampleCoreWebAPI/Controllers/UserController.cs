using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SampleCoreWebAPI.Models;

namespace SampleCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SampleInventoryContext _context;
        private readonly ILogger _logger;

        public UserController(SampleInventoryContext context,ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Route("{id:int}")] // Constrained Route Parameter  ///api/User/5
        public async Task<ActionResult<UserInfo>> Get(int id) 
        {
            string strMessage = $"Request received at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(strMessage);

            _logger.LogInformation($"Start : Getting User details for {id}");

            var users = await _context.UserInfo.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        [Route("Demo")] // Literal Segment  //http://localhost:5000/api/User/Demo
        public async Task<List<UserInfo>> GetNew() 
        {
            var users = _context.UserInfo.AsQueryable();
            users =  _context.UserInfo.Where(user => user.UserName == "Demo");
            return await users.ToListAsync();
        }

        [Route("{UserName}")]  // Unconstrained Route Parameter  //http://localhost:5000/api/User/ankitkt
        public async Task<List<UserInfo>> GetByUserName(string UserName)
        {
            var users = _context.UserInfo.AsQueryable();
            users = _context.UserInfo.Where(user => user.UserName == UserName);
            return await users.ToListAsync();
        }

        [HttpGet("{*date:datetime}")] // constrained Wildcard Parameter
        public async Task<List<UserInfo>> GetByCreationDate(DateTime filterdate)
        {
            var users = _context.UserInfo.AsQueryable();
            users = _context.UserInfo.Where(user => (user.CreatedDate.Date.Day == filterdate.Date.Day &&
                                            user.CreatedDate.Date.Month == filterdate.Date.Month &&
                                            user.CreatedDate.Date.Year == filterdate.Date.Year)
            );
            return await users.ToListAsync();
        }

        //[Route("{*userName}")] // Unconstrained Wildcard Parameter
        //public async Task<List<UserInfo>> GetDefault(string UserName)
        //{
        //    var users = _context.UserInfo.AsQueryable();
        //    users = _context.UserInfo.Where(user => user.UserName.StartsWith(UserName));
        //    return await users.ToListAsync();
        //}

    }
}