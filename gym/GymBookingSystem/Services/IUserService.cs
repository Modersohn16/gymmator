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
    }
}
