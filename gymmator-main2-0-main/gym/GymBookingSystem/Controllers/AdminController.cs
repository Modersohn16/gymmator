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
            User u = _AdminService.CreateUser(dto);
            if (u == null)
            {
                return BadRequest();
            }
            else
            {
                return Created("User created successfully.", u);
            }
        }

        [Authorize]
        [HttpGet("GetUsersBookings")]
        public IActionResult GetUsersBookings(int userId)
        {
            List<Booking> b = _AdminService.GetUsersBookings(userId);
            if (b == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(b);
            }
        }


        [Authorize(Roles = Role.Admin)]
        [HttpPost("CreateTrainer")]
        public IActionResult CreateTrainer(TrainerDto dto)
        {
            Trainer t = _AdminService.CreateTrainer(dto);
            if (t == null)
            {
                return BadRequest();
            }
            else
            {
                return Created("Trainer created successfully.", t);
            }
        }

        [Authorize]
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int UserId)
        {
            User U = _AdminService.DeleteUser(UserId);
            if (U == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(U);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteTrainer")]
        public IActionResult DeleteTrainer(int TrainerId)
        {
            Trainer t = _AdminService.DeleteTrainer(TrainerId);
            if (t == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(t);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateTrainer")]
        public IActionResult UpdateTrainer(int trainerId, TrainerDto dto)
        {
            Trainer t = _AdminService.UpdateTrainer(trainerId, dto);
            if (t == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(t);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("ChangeRole")]
        public IActionResult ChangeRole(int userId, string title)
        {
            User u = _AdminService.ChangeRole(userId, title);
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
