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
            _context.Users.Add(U);
            _context.SaveChanges();

            lc.UserId = U.UserId;
            lc.Username = dto.Username;
            lc.Password = dto.Password;

            _context.LoginCredentials.Add(lc);
            _context.SaveChanges();
            return U;
        }


        public int Login(string username, string password)
        {         
           LoginCredentials lc = _context.LoginCredentials.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
           
            if (lc != null)
                return lc.UserId;
           else
                return 0;
        }

        public string ChangePassword(int userId, string newPassword, string oldPassword)
        {
            LoginCredentials lc = _context.LoginCredentials.Where(x => x.UserId == userId && x.Password == oldPassword).FirstOrDefault();
            
            if (lc != null)
            {
                lc.Password = newPassword;
                _context.Update(lc);
                _context.SaveChanges();
                return "Password changed successfully";
            }
            else
            {
                return "Failed to change password ";
            }
        }
    }
}
