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

        TrainingClass GetTrainingClass(int Id);
        List<TrainingClass> GetTrainingClasses();
        List<TrainingClass> GetTrainingClassesAtGym(int GymId);
        List<TrainingClass> GetTrainingClassesAtDate(int year, int month, int day);
        User DeleteUser(int UserId);
        Booking CreateBooking(BookingDto dto);
        List<Booking> GetUsersBookings(int userId);

        Gym GetGym(int Id);
        List<Gym> GetGyms();

    }
}
