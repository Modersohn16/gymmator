using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;
using GymBookingSystem.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace GymBookingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly GymContext _context;
        private readonly string Secret1;
        private readonly IConfiguration _config;
        private readonly IHasher _hasher;
        public UserService(GymContext context, IHasher hasher)
        {
            _context = context;
            _hasher = hasher;
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
            string hash = _hasher.CreateHash(dto.Password);
            lc.PasswordHash = hash;

            _context.LoginCredentials.Add(lc);
            _context.SaveChanges();
            return U;
        }


        public User Login(string username, string password)
        {         
           LoginCredentials lc = _context.LoginCredentials.Where(x => x.Username == username).FirstOrDefault();

            if (lc == null)
            {
                return null;
            }

            if (!AllowedToLogin(lc))
            {
                UpdateLoginAttempts(lc);
                return null;
            }

            if (!_hasher.ValidatePassword(password, lc.PasswordHash))
            {
                UpdateLoginAttempts(lc);
                return null;
            }

            User u =  _context.Users.Where(x => x.UserId == lc.UserId).FirstOrDefault();
            ResetLoginAttempts(lc);
            return u;

            /*
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret1);
            */
        }

        private bool AllowedToLogin(LoginCredentials lc)
        {
            if (lc.Attempts >= 5)
            {
                if (lc.LastAttempt.AddMinutes(15) < DateTime.UtcNow)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        private void UpdateLoginAttempts(LoginCredentials lc)
        {

                DateTime dt = new DateTime(DateTime.UtcNow.Ticks, DateTimeKind.Utc);
                lc.Attempts++;
                lc.LastAttempt = dt;
                 _context.SaveChanges();
        }

        private void ResetLoginAttempts(LoginCredentials lc)
        {
            lc.Attempts = 0;
            _context.SaveChanges();

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
            {
                return "Failed to change password ";
            }
        }

        public Gym CreateGym(GymDto dto)
        {
            Gym G = new Gym()
            {
                Name = dto.Name,
                StreetAdress = dto.StreetAdress,
                PostalCode = dto.PostalCode,
                City = dto.City,
                OperationalHours = dto.OperationalHours,
                MaxPeople = dto.MaxPeople,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Gyms.Add(G);
            _context.SaveChanges();

            return G;
        
        }


        public TrainingClass CreateTrainingClass(TrainingClassDto dto)
        {
            TrainingClass tc = new TrainingClass()
            {
                    //TrainingClassId = dto.TrainingClassId,
                    GymId = dto.GymId,
                    Name = dto.Name,
                    TrainerId = dto.TrainerId,
                    MaxPeople = dto.MaxPeople,
                    Description = dto.Description,
                    Start = dto.Start,
                    End = dto.End
            };

            _context.TrainingClasses.Add(tc);
            _context.SaveChanges();
            return tc;
        }


        public TrainingClass GetTrainingClass(int Id)
        {
            var t = _context.TrainingClasses.Where(x => x.TrainingClassId == Id).FirstOrDefault();
            
            if (t!=null)
            {
                return t;
            }

            else
            {
                return null;
            }
        }


        public List<TrainingClass> GetTrainingClasses()
        {
            List < TrainingClass > t = _context.TrainingClasses.ToList();

            if (t != null)
            {
                return t;
            }

            else
            {
                return null;
            }

        }

        public Gym GetGym(int Id)
        {
            var g = _context.Gyms.Where(x => x.GymId == Id).FirstOrDefault();

            if (g != null)
            {
                return g;
            }

            else
            {
                return null;
            }
        }

        public List<Gym> GetGyms()
        {
            List<Gym> g = _context.Gyms.ToList();

            if (g != null)
            {
                return g;
            }

            else
            {
                return null;
            }

        }

        public List<TrainingClass> GetTrainingClassesAtGym(int GymId)
        {
            List<TrainingClass> t = _context.TrainingClasses.Where(x => x.GymId == GymId).ToList();

            if (t != null)
            {
                return t;
            }

            else
            {
                return null;
            }

        }


        public List<TrainingClass> GetTrainingClassesAtDate(int year, int month, int day)
        {
            DateTime chosenDate = new DateTime(year, month, day);
            List<TrainingClass> t = _context.TrainingClasses.Where(x => x.Start.Date == chosenDate).ToList();

            if (t != null)
            {
                return t;
            }

            else
            {
                return null;
            }

        }

        public Booking CreateBooking(BookingDto dto)
        {
            Booking b = new Booking()
            {
                GymId = dto.GymId,
                UserId = dto.UserId,
                TrainerId = dto.TrainerId,
                Timestamp = dto.Timestamp,
                Date = dto.Date,
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
            {
                return b;
            }

            else
            {
                return null;
            }
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
            List<Booking> b = _context.Bookings.Where(x => x.UserId == UserId).ToList();
            User U = _context.Users.Where(x => x.UserId == UserId).FirstOrDefault();
            LoginCredentials lc = _context.LoginCredentials.Where(x => x.UserId == UserId).FirstOrDefault();

            _context.LoginCredentials.Remove(lc);
            b.ForEach(x => _context.Bookings.Remove(x));
            _context.Users.Remove(U);
            _context.SaveChanges();

            return U;
        }

        //public string ChangeClass(int trainingclassid, string changegymid, string changetrainerid, string changemaxpeople, string changedescription, string changedatetime_start, string changedatetime_end)
        //{
        //    TrainingClass tc = _context.TrainingClasses.Where(x => x.TrainingClassId == trainingclassid && x.GymId == changegymid == && x.TrainerId == changetrainerid == && x.Maxpeople
        //            == changemaxpeople && x.Description == changedescription && x.DateTime_Start == changedatetime_start && x.DateTime_End == changedatetime_end).FirstOrDefault();

        //    if (tc != null)
        //    {
        //        //tc.TrainingClassId = newTrainingClassId;
        //        tc.GymId = changeGymId;
        //        tc.TrainerId = changeTrainerId;
        //        tc.Maxpeople = changeMaxPeople;
        //        tc.Description = changeDescription;
        //        tc.DateTime_Start = changeDateTime_Start;
        //        tc.DateTime_End = changeDateTime_End;
        //        //lc.Password = newPassword;
        //        _context.Update(tc);
        //        _context.SaveChanges();
        //        return "Class changed successfully";
        //    }
        //    else
        //    {
        //        return "Failed to change class ";
        //    }
        //}




        /*public TrainingClass CreateTrainingClass(TrainingClass dto)
        {
            TrainingClass TC = new TrainingClass()
            {
            TrainingClassId = dto.TrainingClassId,
            GymId = dto.GymId,
            TrainerId = dto.TrainerId,
            Maxpeople = dto.MaxPeople,
            Description = dto.Description,
            DateTime Start = dto.DateTime Start,
            DateTime End = dto.DateTime End
            };

            _context.Gyms.Add(TC);
            _context.SaveChanges();

            return TC;*/
    }
}

