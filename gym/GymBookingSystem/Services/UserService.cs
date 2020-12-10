using GymBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly GymContext _context;

        public UserService(GymContext context)
        {
            _context = context;
        }

        public User CreateUser(UserDto dto)
        {
            User U = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Admin = dto.Admin
            };

            LoginCredentials lc = new LoginCredentials();
            lc.Username = dto.Username;
            lc.Password = dto.Password;

            _context.Users.Add(U);
            _context.LoginCredentials.Add(lc);
            _context.SaveChanges();
            return U;
        }
    }
}
