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

        public string Name { get; set; }
        public string StreetAdress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public int MaxPeople { get; set; }
        public string OperationalHours { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

 

    }
}
