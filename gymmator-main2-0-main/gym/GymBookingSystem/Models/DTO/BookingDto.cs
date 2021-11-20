using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Models.DTO
{
    public class BookingDto
    {
        // Foreign key to Gym
        public int GymId { get; set; }
        // Foreign key to User
        public int UserId { get; set; }
        public int? TrainerId { get; set; }
        public int? TrainingClassId { get; set; }
    }
}
