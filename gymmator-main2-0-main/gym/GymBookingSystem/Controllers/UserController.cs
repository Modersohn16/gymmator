using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;
using GymBookingSystem.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


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


        [HttpPost("CreateUser")]
        public IActionResult CreateUser(UserDto dto)
        {
            User u = _UserService.CreateUser(dto);
            if (u == null)
            {
                return BadRequest();
            }
            else
            {
                return Created("User created successfully.", u);
            }
        }
    
        
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginModel login) 
        {
            User user = _UserService.Login(login.Username, login.Password);
            if(user == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(user);
            }
        }

        [Authorize]
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

        [HttpGet("GetTrainingClass")]
        public IActionResult GetTrainingClass(int Id)
        {
            TrainingClass t = _UserService.GetTrainingClass(Id);
            if (t == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(t);
            }
        }

        [HttpGet("GetTrainingClasses")]
        public IActionResult GetTrainingClasses()
        {
            List<TrainingClass> t = _UserService.GetTrainingClasses();
            if (t == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(t);
            }
        }


        [HttpGet("GetTrainingClassesAtDate")]
        public IActionResult GetTrainingClassesAtDate(int year, int month, int day)
        {
            
            List<TrainingClass> t = _UserService.GetTrainingClassesAtDate(year, month, day);
            if (t == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(t);
            }
        }

        [EnableCors("MyPolicy")]
        [HttpGet("GetGym")]
        public IActionResult GetGym()
        {
            List<Gym> g = _UserService.GetGyms();
            if (g == null)
            {
                return BadRequest();
            }
            else return Ok(g);
        }

        [HttpGet("GetTrainingClassesAtGym")]
        public IActionResult GetTrainingClassesAtGym(int GymId)
        {
            List<TrainingClass> t = _UserService.GetTrainingClassesAtGym(GymId);
            if (t == null)
            {
                return BadRequest();
            }

            else
            {
                return Ok(t);
            }
        }

        [Authorize]
        [HttpPost("CreateBooking")]
        public IActionResult CreateBooking(BookingDto dto)
        {
            Booking b = _UserService.CreateBooking(dto);
            if (b == null)
            {
                return BadRequest();
            }
            else
            {
                return Created("yay", b);
            }
        }

        [Authorize]
        [HttpGet("GetUsersBookings")]
        public IActionResult GetUsersBookings(int userId)
        {
            List<Booking> b = _UserService.GetUsersBookings(userId);
            if (b == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(b);
            }
        }

        [Authorize]
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int UserId)
        {
            User U = _UserService.DeleteUser(UserId);
            if (U == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(U);
            }
        }
    }
}
