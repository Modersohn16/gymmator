using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Models
{
    public class User
    {
        //PrimaryKey
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        [NotMapped]
        public Role Role { get; set; }
        public string Token { get; set; }

        public User(Role r)
        {
            Role = r;
        }

        public User()
        {

        }
    }
}
