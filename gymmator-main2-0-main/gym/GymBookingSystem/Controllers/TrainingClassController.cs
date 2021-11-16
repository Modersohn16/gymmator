using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;
using GymBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace GymBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingClassController : ControllerBase
    {
        private ITrainingClassService _TrainingClassService;
        public TrainingClassController(ITrainingClassService TrainingClassService)
        {
            _TrainingClassService = TrainingClassService;
        }

        [HttpGet("GetTrainingClass")]
        public IActionResult GetTrainingClass(int Id)
        {
            TrainingClass t = _TrainingClassService.GetTrainingClass(Id);
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
            List<TrainingClass> t = _TrainingClassService.GetTrainingClasses();
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

            List<TrainingClass> t = _TrainingClassService.GetTrainingClassesAtDate(year, month, day);
            if (t == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(t);
            }
        }

        [HttpGet("GetTrainingClassesAtGym")]
        public IActionResult GetTrainingClassesAtGym(int GymId)
        {
            List<TrainingClass> t = _TrainingClassService.GetTrainingClassesAtGym(GymId);
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
        [HttpPost("CreateTrainingClass")]
        public IActionResult CreateTrainingClass(TrainingClassDto dto)
        {
            TrainingClass t = _TrainingClassService.CreateTrainingClass(dto);
            if (t == null)
            {
                return BadRequest();
            }
            else
            {
                return Created("Training class created successfully.", t);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteTrainingClass")]
        public IActionResult DeleteTrainingClass(int TrainingClassId)
        {
            TrainingClass t = _TrainingClassService.DeleteTrainingClass(TrainingClassId);
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
        [HttpPut("UpdateTrainingClass")]
        public IActionResult UpdateTrainingClass(int TrainingClassId, TrainingClassDto dto)
        {
            TrainingClass t = _TrainingClassService.UpdateTrainingClass(TrainingClassId, dto);
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
