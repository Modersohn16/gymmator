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
    
        
        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginDto dto) 
        {
            User user = _UserService.Login(dto);
            if(user == null)
            {
                return BadRequest("Invalid username or password");
            }
            else
            {
                return Ok(user);
            }
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword([FromBody]ChangePasswordDto dto)
        {
            string message = _UserService.ChangePassword(dto.UserId, dto.NewPassword, dto.OldPassword);
            if(message.Contains("successfully"))
            {
                return Ok(message);
            }
            else
            {
                return BadRequest();
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
