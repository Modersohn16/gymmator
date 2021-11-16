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
using Microsoft.Extensions.Logging;
using Serilog;

namespace GymBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _UserService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService UserService, ILogger<UserController> logger)
        {
            _UserService = UserService;
            _logger = logger;
        }


        [HttpPost("CreateUser")]
        public IActionResult CreateUser(UserDto dto)
        {
            Log.Information("Attempting to create user: " + dto);
            User u = _UserService.CreateUser(dto);
            if (u == null)
            {
                Log.Warning("Failed to create user" + dto);
                return BadRequest();
            }
            else
            {
                Log.Information("User created successfully: " + u);
                return Created("User created successfully.", u);
            }
        }
    
        
        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginDto dto) 
        {
            Log.Information("Authenticating user: " + dto.Username);
            User user = _UserService.Login(dto);
            if(user == null)
            {
                Log.Information("Failed to authenticate user: " + dto.Username);
                return BadRequest("Invalid username or password");
            }
            else
            {
                Log.Information("Authenticated user: " + dto.Username);
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

        [Authorize]
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(int userId, UpdateUserDto dto)
        {
            User u = _UserService.UpdateUser(userId, dto);
            if (u == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(u);
            }
        }
    }
}
