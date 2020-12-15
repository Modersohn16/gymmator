using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Models
{
    public class TrainingClass
    {
        // Primary key
        public int TrainingClassId { get; set; }
        // Foreign key
        public int GymId { get; set; }
        // Foreign key
        public int TrainerId { get; set; }
        public int MaxPeople { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
