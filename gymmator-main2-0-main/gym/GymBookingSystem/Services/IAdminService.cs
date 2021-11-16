using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;

namespace GymBookingSystem.Services
{
    public interface IAdminService
    {
        User CreateUser(UserDto dto);
        User DeleteUser(int UserId);
        List<Booking> GetUsersBookings(int userId);
        Trainer CreateTrainer(TrainerDto dto);
        Trainer DeleteTrainer(int TrainerId);
        Trainer UpdateTrainer(int trainerId, TrainerDto dto);
    }
}
