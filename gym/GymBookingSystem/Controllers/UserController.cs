using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBookingSystem.Models;
using GymBookingSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _UserService; 
        public UserController(IUserService UserService)
        {
            _UserService = UserService;
        }

        [HttpPost("create")]
        public IActionResult CreateUser(UserDto dto)
        {
            User u = _UserService.CreateUser(dto);
            if (u == null)
            {
                return BadRequest();
            }
            else
            {
                return Created("yay", u);
            }
        }
        
        [HttpGet("login")]
        public IActionResult Login(string username, string password) 
        {
            int userID = _UserService.Login(username, password);
            if(userID == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(userID);
            }

        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(int userId, string newPassword, string oldPassword)
        {
            string message = _UserService.ChangePassword(userId, newPassword, oldPassword);
            if(message.Contains("successfully"))
            {
                return Ok(message);
            }
            else
            {
                return BadRequest();
            }

        }




            //// GET: api/User
            //[HttpGet]
            //public IEnumerable<string> Get()
            //{
            //    return new string[] { "value1", "value2" };
            //}

            //// GET: api/User/5
            //[HttpGet("{id}", Name = "Get")]
            //public string Get(int id)
            //{
            //    return "value";
            //}

            //// POST: api/User
            //[HttpPost]
            //public void Post([FromBody] string value)
            //{
            //}

            //// PUT: api/User/5
            //[HttpPut("{id}")]
            //public void Put(int id, [FromBody] string value)
            //{
            //}

            //// DELETE: api/ApiWithActions/5
            //[HttpDelete("{id}")]
            //public void Delete(int id)
            //{
            //}
        }
    }
