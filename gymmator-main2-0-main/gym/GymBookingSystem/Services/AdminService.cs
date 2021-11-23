using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;
using GymBookingSystem.Cryptography;

namespace GymBookingSystem.Services
{
    public class AdminService : IAdminService
    {
        private readonly GymContext _context;
        private readonly IHasher _hasher;

        public AdminService(GymContext context, IHasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public User CreateUser(UserDto dto)
        {
            var role = _context.Roles.Where(x => x.Title == dto.Title).FirstOrDefault();
            User U = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Role = role,
                RoleId = role.Id  
            };

            LoginCredentials lc = new LoginCredentials();
            _context.Users.Add(U);
            _context.SaveChanges();

            lc.UserId = U.UserId;
            lc.Username = dto.Username;
            string hash = _hasher.CreateHash(dto.Password);
            lc.PasswordHash = hash;

            _context.LoginCredentials.Add(lc);
            _context.SaveChanges();
            return U;
        }


        public User Login(string username, string password)
        {
            LoginCredentials lc = _context.LoginCredentials.Where(x => x.Username == username && x.PasswordHash == password).FirstOrDefault();

            if (lc != null)
                return _context.Users.Where(x => x.UserId == lc.UserId).FirstOrDefault();
            else
                return null;
        }

        public string ChangePassword(int userId, string newPassword, string oldPassword)
        {
            LoginCredentials lc = _context.LoginCredentials.Where(x => x.UserId == userId && x.PasswordHash == oldPassword).FirstOrDefault();

            if (lc != null)
            {
                lc.PasswordHash = newPassword;
                _context.Update(lc);
                _context.SaveChanges();
                return "Password changed successfully";
            }
            else
                return "Failed to change password ";
        }



        public List<TrainingClass> GetTrainingClassesAtGym(int GymId)
        {
            List<TrainingClass> t = _context.TrainingClasses.Where(x => x.GymId == GymId).ToList();

            if (t != null)
                return t;

            else
                return null;
        }

        public Booking CreateBooking(BookingDto dto)
        {
            TrainingClass t = _context.TrainingClasses.Where(x => x.TrainingClassId == dto.TrainingClassId).FirstOrDefault();

            if (t == null)
                return null;

            Booking b = new Booking()
            {
                GymId = dto.GymId,
                UserId = dto.UserId,
                TrainerId = dto.TrainerId,
                Timestamp = DateTime.Now,
                Date = t.Start,
                TrainingClassId = dto.TrainingClassId
            };

            _context.Bookings.Add(b);
            _context.SaveChanges();
            return b;
        }

        public List<Booking> GetUsersBookings(int userId)
        {
            List<Booking> b = _context.Bookings.Where(x => x.UserId == userId).ToList();

            if (b != null)
                return b;
            else
                return null;
        }

        public Trainer CreateTrainer(TrainerDto dto)
        {
            Trainer t = new Trainer()
            {

                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
            _context.Trainers.Add(t);
            _context.SaveChanges();
            return t;

        }

        public User DeleteUser(int UserId)
        {
            User U = _context.Users.Where(x => x.UserId == UserId).FirstOrDefault();

            if (U == null)
                return null;

            List<Booking> b = _context.Bookings.Where(x => x.UserId == UserId).ToList();
            LoginCredentials lc = _context.LoginCredentials.Where(x => x.UserId == UserId).FirstOrDefault();

            _context.LoginCredentials.Remove(lc);
            b.ForEach(x => _context.Bookings.Remove(x));
            _context.Users.Remove(U);
            _context.SaveChanges();
            return U;
        }
        public Trainer DeleteTrainer(int trainerId)
        {
            Trainer t = _context.Trainers.Where(x => x.TrainerId == trainerId).FirstOrDefault();

            if (t == null)
                return null;

            _context.Trainers.Remove(t);
            _context.SaveChanges();
            return t;
        }
        public Trainer UpdateTrainer(int trainerId, TrainerDto dto)
        {
            Trainer t = _context.Trainers.Where(x => x.TrainerId == trainerId).FirstOrDefault();

            if (t == null)
                return null;

            t.FirstName = dto.FirstName;
            t.LastName = dto.LastName;
            t.Email = dto.Email;
            _context.Update(t);
            _context.SaveChanges();
            return t;
        }

        public ChangeRoleDto ChangeRole(int userId, string title)
        {
            User u = _context.Users.Where(x => x.UserId == userId).FirstOrDefault();

            if (u == null)
                return null;

            var userRole = _context.Roles.Where(x => x.Title == title).FirstOrDefault();
            u.Role = userRole;
            u.RoleId = userRole.Id;
            ChangeRoleDto dto = new ChangeRoleDto();
            dto.UserId = u.UserId;
            dto.FirstName = u.FirstName;
            dto.LastName = u.LastName;
            dto.Email = u.Email;
            dto.Title = u.Role.Title;
           
            return dto;
        }
    }
}
