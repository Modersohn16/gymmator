using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;
using GymBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Serilog;

namespace GymBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private IGymService _GymService;
        private readonly ILogger<GymController> _logger;

        public GymController(IGymService GymService, ILogger<GymController> logger)
        {
            _GymService = GymService;
            _logger = logger;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("CreateGym")]
        public IActionResult CreateGym(GymDto dto)
        {
            Log.Information("Attempting to create gym.");
            Gym g = _GymService.CreateGym(dto);
            if (g == null)
            {
                Log.Error("Failed to create gym.");
                return BadRequest();
            }
            else
            {
                Log.Information("Gym created successfully.");
                return Created("Gym created successfully.", g);
            }
        }


        [HttpGet("GetGyms")]
        public IActionResult GetGyms()
        {
            Log.Information("Attempting to retrieve gyms.");
            List<Gym> g = _GymService.GetGyms();
            if (g == null)
            {
                Log.Error("Failed to retrieve gyms.");
                return BadRequest();
            }
            else
            {
                Log.Information("Gyms retrieved successfully.");
                return Ok(g);
            }
        }

        [HttpGet("GetGym")]
        public IActionResult GetGym(int GymId)
        {
            Log.Information("Attempting to retrieve gyms.");
            Gym g = _GymService.GetGym(GymId);
            if (g == null)
            {
                Log.Error("Failed to retrieve gym.");
                return BadRequest();
            }
            else
            {
                Log.Information("Gym retrieved successfully.");
                return Ok(g);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteGym")]
        public IActionResult DeleteUser(int GymId)
        {
            Log.Information("Attempting to delete gym.");
            Gym g = _GymService.DeleteGym(GymId);
            if (g == null)
            {
                Log.Error("Failed to delete gym.");
                return BadRequest();
            }
            else
            {
                Log.Information("Gym deleted successfully. " + g);
                return Ok(g);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateGym")]
        public IActionResult UpdateGym(int GymId, GymDto dto)
        {
            Log.Information("Attempting to update gym.");
            Gym g = _GymService.UpdateGym(GymId, dto);
            if (g == null)
            {
                Log.Error("Failed to update gym.");
                return BadRequest();
            }
            else
            {
                Log.Information("Gym updated successfully. " + g);
                return Ok(g);
            }
        }
    }
}
