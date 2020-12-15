using GymBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Services
{
    public interface IUserService
    {
        User CreateUser(UserDto dto);
        int Login(string username, string password);
        string ChangePassword(int userId, string newPassword, string oldPassword);
        Gym CreateGym(GymDto dto);
    }
}
