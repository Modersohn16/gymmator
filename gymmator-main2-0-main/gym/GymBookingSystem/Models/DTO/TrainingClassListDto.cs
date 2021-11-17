using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Models.DTO
{
    public class TrainingClassListDto
    {
        public int GymId { get; set; }
        public string Name { get; set; }
        public List<TrainingClass> Classes { get; set; }
    }
}
