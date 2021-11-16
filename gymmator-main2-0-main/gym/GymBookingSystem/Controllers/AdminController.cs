using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;
using GymBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace GymBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _AdminService;
        public AdminController(IAdminService AdminService)
        {
            _AdminService = AdminService;
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
        public IActionResult UpdateGym(int trainerId, TrainerDto dto)
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
    }
}
