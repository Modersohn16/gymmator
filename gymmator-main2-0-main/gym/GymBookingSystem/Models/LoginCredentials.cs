using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Models
{
    public class LoginCredentials
    {
        // Primary key
        public int LoginCredentialsId { get; set; }
        // Foreign key to user
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime LastAttempt { get; set; }
        public int Attempts { get; set; }
    }
}
