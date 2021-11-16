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

namespace GymBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private IGymService _GymService;


        public GymController(IGymService GymService)
        {
            _GymService = GymService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("CreateGym")]
        public IActionResult CreateGym(GymDto dto)
        {
            Gym g = _GymService.CreateGym(dto);
            if (g == null)
            {
                return BadRequest();
            }
            else
            {
                return Created("Gym created successfully.", g);
            }
        }

        [EnableCors("MyPolicy")]
        [HttpGet("GetGyms")]
        public IActionResult GetGyms()
        {
            List<Gym> g = _GymService.GetGyms();
            if (g == null)
            {
                return BadRequest();
            }
            else return Ok(g);
        }

        [HttpGet("GetGym")]
        public IActionResult GetGym(int GymId)
        {
            Gym g = _GymService.GetGym(GymId);
            if (g == null)
            {
                return BadRequest();
            }
            else return Ok(g);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteGym")]
        public IActionResult DeleteUser(int GymId)
        {
            Gym g = _GymService.DeleteGym(GymId);
            if (g == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(g);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateGym")]
        public IActionResult UpdateTrainingClass(int GymId, GymDto dto)
        {
            TrainingClass t = _GymService.UpdateGym(GymId, dto);
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
