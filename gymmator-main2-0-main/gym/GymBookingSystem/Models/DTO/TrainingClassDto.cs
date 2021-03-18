using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Models.DTO
{
    public class TrainingClassDto
    {


        public int GymId { get; set; }
        /*public string ClassName { get; set; }*/
        public string Name { get; set; }

        public int TrainerId { get; set; }
        //public string Participants { get; set; }
        public int MaxPeople { get; set; }
        /*public string ClassHours { get; set; }*/
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}