using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Services
{
    public interface IUserService
    {
        User CreateUser(UserDto dto);
        User Login(string username, string password);
        string ChangePassword(int userId, string newPassword, string oldPassword);
        Gym CreateGym(GymDto dto);

        TrainingClass CreateTrainingClass(TrainingClassDto dto);
        TrainingClass GetTrainingClass(int Id);
        List<TrainingClass> GetTrainingClasses();
        List<TrainingClass> GetTrainingClassesAtGym(int GymId);
        User DeleteUser(int UserId);
        Booking CreateBooking(BookingDto dto);
        List<Booking> GetUsersBookings(int userId);
        Trainer CreateTrainer(TrainerDto dto);
       // List<Trainer> GetTrainers();

        Gym GetGym(int Id);
        List<Gym> GetGyms();

    }
}
