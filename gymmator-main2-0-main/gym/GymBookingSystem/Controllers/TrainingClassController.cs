using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;
using GymBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Serilog;

namespace GymBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingClassController : ControllerBase
    {
        private ITrainingClassService _TrainingClassService;
        private readonly ILogger<TrainingClassController> _logger;
        public TrainingClassController(ITrainingClassService TrainingClassService, ILogger<TrainingClassController> logger)
        {
            _TrainingClassService = TrainingClassService;
            _logger = logger;
        }

        [HttpGet("GetTrainingClass")]
        public IActionResult GetTrainingClass(int Id)
        {
            Log.Information($"Attempting to retrieve training class. Id: {Id}.");
            TrainingClass t = _TrainingClassService.GetTrainingClass(Id);
            if (t == null)
            {
                Log.Error("Failed to retrieve training class.");
                return BadRequest();
            }
            else
            {
                Log.Information("Training class retrieved successfully." + t);
                return Ok(t);
            }
        }

        // Show only available classes (not from previous days)
        // optional with boolean
        [HttpGet("GetTrainingClasses")]
        public IActionResult GetTrainingClasses()
        {
            Log.Information("Attempting to retrieve training classes.");
            List<TrainingClass> t = _TrainingClassService.GetTrainingClasses();
            if (t == null)
            {
                Log.Error("Failed to retrieve training classes.");
                return BadRequest();
            }
            else
            {
                Log.Information("Training classes retrieved successfully." + t);
                return Ok(t);
            }
        }

        [HttpGet("GetTrainingClassesAtDate")]
        public IActionResult GetTrainingClassesAtDate(DateTime dateTime)
        {
            Log.Information($"Attempting to retrieve training classes at date: {dateTime}.");
            List<TrainingClass> t = _TrainingClassService.GetTrainingClassesAtDate(dateTime);
            if (t == null)
            {
                Log.Error("Failed to retrieve training classes.");
                return BadRequest();
            }
            else
            {
                Log.Information("Training classes retrieved successfully. Dto: " + t);
                return Ok(t);
            }
        }

        [HttpGet("GetTrainingClassesAtGym")]
        public IActionResult GetTrainingClassesAtGym(int gymId)
        {
            Log.Information($"Attempting to retrieve training classes at gym id: {gymId}.");
            var t = _TrainingClassService.GetTrainingClassesAtGym(gymId);

            if (t.Name == null || t.Name.Length < 1)
            {
                Log.Error("Failed to find gym for training classes");
                return NotFound("Failed to find gym for training classes");
            }
            if(t.Classes == null || t.Classes.Count < 1)
            {
                Log.Error("Failed to find training classes");
                return BadRequest("Failed to find training classes");
            }
            else
            {
                Log.Information("Training classes retrieved successfully. Dto: " + t);
                return Ok(t);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("CreateTrainingClass")]
        public IActionResult CreateTrainingClass(TrainingClassDto dto)
        {
            Log.Information("Attempting to create training class.");
            TrainingClass t = _TrainingClassService.CreateTrainingClass(dto);
            if (t == null)
            {
                Log.Error("Failed to create training class.");
                return BadRequest();
            }
            else
            {
                Log.Information("Training class created successfully. " + t);
                return Created("Training class created successfully.", t);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteTrainingClass")]
        public IActionResult DeleteTrainingClass(int TrainingClassId)
        {
            Log.Information("Attempting to delete training class.");
            TrainingClass t = _TrainingClassService.DeleteTrainingClass(TrainingClassId);
            if (t == null)
            {
                Log.Error("Failed to delete training class.");
                return BadRequest();
            }
            else
            {
                Log.Information("Training class deleted successfully. " + t);
                return Ok(t);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateTrainingClass")]
        public IActionResult UpdateTrainingClass(int TrainingClassId, TrainingClassDto dto)
        {
            Log.Information("Attempting to update training class.");
            TrainingClass t = _TrainingClassService.UpdateTrainingClass(TrainingClassId, dto);
            if (t == null)
            {
                Log.Error("Failed to update training class.");
                return BadRequest();
            }
            else
            {
                Log.Information("Training class updated successfully. " + t);
                return Ok(t);
            }
        }
    }
}
