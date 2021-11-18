using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;
using GymBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Serilog;

namespace GymBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _AdminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService AdminService, ILogger<AdminController> logger)
        {
            _AdminService = AdminService;
            _logger = logger;
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser(UserDto dto)
        {
            Log.Information("Attempting to create user: " + dto);
            User u = _AdminService.CreateUser(dto);
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

        [Authorize]
        [HttpGet("GetUsersBookings")]
        public IActionResult GetUsersBookings(int userId)
        {
            Log.Information("Attempting to retrieve user's bookings, userId: " + userId);
            List<Booking> b = _AdminService.GetUsersBookings(userId);
            if (b == null)
            {
                Log.Error("Failed to retrieve user's bookings, userId: " + userId);

                return BadRequest();
            }
            else
            {
                Log.Information("User's bookings retrieved successfully. " + b);
                return Ok(b);
            }
        }


        [Authorize(Roles = Role.Admin)]
        [HttpPost("CreateTrainer")]
        public IActionResult CreateTrainer(TrainerDto dto)
        {
            Log.Information("Attempting to create trainer.");
            Trainer t = _AdminService.CreateTrainer(dto);
            if (t == null)
            {
                Log.Error("Failed to create trainer.");
                return BadRequest();
            }
            else
            {
                Log.Information("Trainer created successfully. " + t);
                return Created("Trainer created successfully.", t);
            }
        }

        [Authorize]
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int userId)
        {
            Log.Information("Attempting to delete user. UserID: " + userId);
            User U = _AdminService.DeleteUser(userId);
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

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteTrainer")]
        public IActionResult DeleteTrainer(int trainerId)
        {
            Log.Information("Attempting to delete trainer. TrainerID: " + trainerId);
            Trainer t = _AdminService.DeleteTrainer(trainerId);
            if (t == null)
            {
                Log.Error("Failed to delete trainer.");
                return BadRequest();
            }
            else
            {
                Log.Information("Trainer deleted successfully. " + t);
                return Ok(t);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateTrainer")]
        public IActionResult UpdateTrainer(int trainerId, TrainerDto dto)
        {
            Log.Information("Attempting to update trainer. TrainerID: " + trainerId);
            Trainer t = _AdminService.UpdateTrainer(trainerId, dto);
            if (t == null)
            {
                Log.Error("Failed to update trainer.");
                return BadRequest();
            }
            else
            {
                Log.Information("Trainer updated successfully. " + t);
                return Ok(t);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("ChangeRole")]
        public IActionResult ChangeRole(int userId, string title)
        {
            Log.Information("Attempting to change role of user" + userId + " to " + title);
            User u = _AdminService.ChangeRole(userId, title);
            if (u == null)
            {
                Log.Error("Failed to change role.");
                return BadRequest();
            }
            else
            {
                Log.Information("Role changed successfully. " + u);
                return Ok(u);
            }
        }
    }
}
