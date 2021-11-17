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
    public class UserController : ControllerBase2
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
                Log.Error("Failed to create user" + dto);
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
                Log.Warning("Failed to authenticate user: " + dto.Username);
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
            Log.Information("Attempting to change password.");
            string message = _UserService.ChangePassword(dto.UserId, dto.NewPassword, dto.OldPassword);
            if(message.Contains("successfully"))
            {
                Log.Information("Password changed successfully.");
                return Ok(message);
            }
            else
            {
                Log.Error("Failed to change password.");
                return BadRequest();
            }

        }

        [Authorize]
        [HttpPost("CreateBooking")]
        public IActionResult CreateBooking(BookingDto dto)
        {
            Log.Information("Attempting to create booking.");
            Booking b = _UserService.CreateBooking(dto);
            if (b == null)
            {
                Log.Error("Failed to create booking.");
                return BadRequest();
            }
            else
            {
                Log.Information("Booking created successfully. " + b);
                return Created("yay", b);
            }
        }

        [Authorize]
        [HttpGet("GetUsersBookings")]
        public IActionResult GetUsersBookings()
        {
            Log.Information("Attempting to retrieve user bookings.");
            int userId = GetUserId();
            List<Booking> b = _UserService.GetUsersBookings(userId);
            if (b == null)
            {
                Log.Error("Failed to retrieve user bookings.");
                return BadRequest();
            }
            else
            {
                Log.Information("Booking retrieved successfully. " + b);
                return Ok(b);
            }
        }

        [Authorize]
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int UserId)
        {
            Log.Information("Attempting to delete user.");
            User U = _UserService.DeleteUser(UserId);
            if (U == null)
            {
                Log.Error("Failed to delete user.");
                return BadRequest();
            }
            else
            {
                Log.Information("User deleted successfully. " + U);
                return Ok(U);
            }
        }

        [Authorize]
        [HttpDelete("DeleteBooking")]
        public IActionResult DeleteBooking(int bookingId)
        {
            Log.Information("Attempting to delete booking.");
            Booking b = _UserService.DeleteBooking(bookingId);
            if (b == null)
            {
                Log.Error("Failed to delete booking.");
                return BadRequest();
            }
            else
            {
                Log.Information("booking deleted successfully. " + b);
                return Ok(b);
            }
        }

        [Authorize]
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(int userId, UpdateUserDto dto)
        {
            Log.Information("Attempting to update user: " + dto);
            User u = _UserService.UpdateUser(userId, dto);
            if (u == null)
            {
                Log.Error("Failed to update user" + dto);
                return BadRequest();
            }
            else
            {
                Log.Information("User updated successfully: " + u);
                return Ok(u);
            }
        }

        [Authorize]
        [HttpPut("UpdateBooking")]
        public IActionResult UpdateBooking(int bookingId, BookingDto dto)
        {
            Log.Information("Attempting to update booking: " + dto);
            Booking b = _UserService.UpdateBooking(bookingId, dto);
            if (b == null)
            {
                Log.Error("Failed to update booking" + dto);
                return BadRequest();
            }
            else
            {
                Log.Information("Booking updated successfully: " + b);
                return Ok(b);
            }
        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string username, string email)
        {
            Log.Information($"Attempting to reset password for user {username}.");
            var t = _UserService.ResetPassword(username, email);
            if (t == null)
            {
                Log.Error("Failed to reset password.");
                return BadRequest();
            }
            else
            {
                Log.Information("Password reset successfully.");
                return Ok(t);
            }
        }
    }
}
