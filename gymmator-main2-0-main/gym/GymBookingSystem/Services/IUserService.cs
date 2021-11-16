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
        User Login(LoginDto dto);
        string ChangePassword(int userId, string newPassword, string oldPassword);
        User DeleteUser(int UserId);
        Booking CreateBooking(BookingDto dto);
        List<Booking> GetUsersBookings(int userId);



    }
}
