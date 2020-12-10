using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Models
{
    public class Gym
    {
        // Primary key
        public int GymId { get; set; }
        public string GymAdress { get; set; }
        public int MaxPeople { get; set; }
        public string OperationalHours { get; set; }


    }
}
