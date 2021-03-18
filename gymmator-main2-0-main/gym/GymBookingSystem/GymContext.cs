using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using GymBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using GymBookingSystem.Models.DTO;

namespace GymBookingSystem
{
    public class GymContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<LoginCredentials> LoginCredentials { get; set; }
        public DbSet<TrainingClass> TrainingClasses { get; set; }
        public GymContext(DbContextOptions<GymContext> options) : base(options) { }

        public DbSet<Trainer> Trainers { get; set; }
    }
}
