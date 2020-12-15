using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Models
{
    public class Booking
    {
        // Primary Key
        public int BookingId { get; set; }
        // Foreign key to Gym
        public int GymId { get; set; }
        // Foreign key to User
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime Date { get; set; }
        public int? TrainerId { get; set; }
        public int? TrainingClassId { get; set; }

    }
}
