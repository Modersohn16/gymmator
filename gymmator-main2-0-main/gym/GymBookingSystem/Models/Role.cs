using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Models
{
    public class Role
    {
        // Primary key
        public int Id { get; set; }
        public string Title { get; set; }

        [NotMapped]
        public const string Admin = "Admin";
        [NotMapped]
        public const string User = "User";

    }
}
