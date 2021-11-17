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
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace GymBookingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly GymContext _context;
        private readonly string Secret1;
        private readonly IConfiguration _config;
        private readonly IHasher _hasher;
        private readonly int EmailPort;
        private readonly string EmailHost;
        private readonly string EmailPassword;
        private readonly string SenderEmail;

        public UserService(GymContext context, IHasher hasher, IConfiguration config)
        {
            _config = config;
            _context = context;
            _hasher = hasher;
            Secret1 = _config.GetConnectionString("Secret1");
            EmailPort = int.Parse(_config.GetConnectionString("EmailPort"));
            EmailPassword = _config.GetConnectionString("EmailPassword");
            SenderEmail = _config.GetConnectionString("SenderEmail");
            EmailHost = _config.GetConnectionString("EmailHost");

        }

        public User CreateUser(UserDto dto)
        {
            if (!ValidUserDto(dto))
                return null;

            if (!PasswordFollowsGuidelines(dto.Password))
                return null;

            if (UsernameExists(dto.Username))
                return null;

            var userRole = _context.Roles.Where(x => x.Title == "User").FirstOrDefault();

            User U = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Role = userRole,
                RoleId = userRole.Id
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

        private bool UsernameExists(string name)
        {
            LoginCredentials lc = _context.LoginCredentials.Where(x => x.Username == name).FirstOrDefault();
            if (lc != null)
                    return true;
                else
                    return false;            
        }
        /*
         * Password requirements: 8 - 36 characters. At least:
         * 1 capital letter
         * 1 lower letter
         * 1 special character
         */
        private bool PasswordFollowsGuidelines(string pass)
        {
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (pass.Length < 8 || pass.Length > 36)
                return false;
            if (!pass.Any(char.IsDigit))
                return false;
            if (!pass.Any(char.IsLower))
                return false;
            if (!pass.Any(char.IsUpper))
                return false;
            if (!hasSymbols.IsMatch(pass))
                return false;

            return true;
        }

        private bool ValidUserDto(UserDto dto)
        {
            if (string.IsNullOrEmpty(dto.FirstName))
                return false;

            if (string.IsNullOrEmpty(dto.LastName))
                return false;

            if (string.IsNullOrEmpty(dto.Email))
                return false;

            if (string.IsNullOrEmpty(dto.Username))
                return false;

            return true;
        }

        public User Login(LoginDto dto)
        {
            if (!ValidLoginDto(dto))
                return null;
           LoginCredentials lc = _context.LoginCredentials.Where(x => x.Username == dto.Username).FirstOrDefault();

            if (lc == null)
            {
                return null;
            }

            if (!AllowedToLogin(lc))
            {
                UpdateLoginAttempts(lc);
                return null;
            }

            if (!_hasher.ValidatePassword(dto.Password, lc.PasswordHash))
            {
                UpdateLoginAttempts(lc);
                return null;
            }

            User u = _context.Users.Where(x => x.UserId == lc.UserId).FirstOrDefault();
            Role r = _context.Roles.Where(x => x.Id == u.RoleId).FirstOrDefault();

            u.Role = new Role();
            u.Role.Id = r.Id;
            u.Role.Title = r.Title;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, u.UserId.ToString()),
                    new Claim(ClaimTypes.Role, r.Title)
        }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            u.Token = tokenHandler.WriteToken(token);

            ResetLoginAttempts(lc);
            return u;
        }
        private bool ValidLoginDto(LoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.Password) || dto.Password.Length < 8)
                return false;

            if (string.IsNullOrEmpty(dto.Username))
                return false;

            return true;
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
            if(oldPassword == newPassword)
            {
                return "Old password and new password are same";
            }
            LoginCredentials lc = _context.LoginCredentials.Where(x => x.UserId == userId).FirstOrDefault();

            if(lc == null)
            {
                return "Failed to change password ";
            }

            if (!_hasher.ValidatePassword(oldPassword, lc.PasswordHash))
            {
                return "Incorrect old password";
            }

            var hash = _hasher.CreateHash(newPassword);
            lc.PasswordHash = hash;
            _context.Update(lc);
            _context.SaveChanges();
            return "Password changed successfully";
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
        public Booking UpdateBooking(int bookingId, BookingDto dto)
        {
            Booking b = _context.Bookings.Where(x => x.BookingId == bookingId).FirstOrDefault();

            if (b == null)
            {
                return null;
            }
            b.Date = dto.Date;
            b.GymId = dto.GymId;
            b.Timestamp = dto.Timestamp;
            b.TrainerId = dto.TrainerId;
            b.TrainingClassId = dto.TrainingClassId;
            b.UserId = dto.UserId;
            
            _context.Bookings.Update(b);
            _context.SaveChanges();

            return b;
        }

        public Booking DeleteBooking(int bookingId)
        {
            Booking b = _context.Bookings.Where(x => x.BookingId == bookingId).FirstOrDefault();

            if (b == null)
            {
                return null;
            }
            _context.Bookings.Remove(b);
            _context.SaveChanges();

            return b;
        }

        public User DeleteUser(int UserId)
        {
            List<Booking> b = _context.Bookings.Where(x => x.UserId == UserId).ToList();
            User U = _context.Users.Where(x => x.UserId == UserId).FirstOrDefault();
            LoginCredentials lc = _context.LoginCredentials.Where(x => x.UserId == UserId).FirstOrDefault();
            
            if(lc == null)
            {
                return null;
            }
            _context.LoginCredentials.Remove(lc);
            b.ForEach(x => _context.Bookings.Remove(x));
            _context.Users.Remove(U);
            _context.SaveChanges();

            return U;
        }
        public User UpdateUser(int userId, UpdateUserDto dto)
        {
            User u = _context.Users.Where(x => x.UserId == userId).FirstOrDefault();
            if (u == null)
                return null;

            u.FirstName = dto.FirstName;
            u.LastName = dto.LastName;
            u.Email = dto.Email;

            _context.Update(u);
            _context.SaveChanges();
            return u;
        }

        public string ResetPassword(string username, string email)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                var t = ResetPasswordByUsername(username);

                if (t != null)
                    return "Password reset";
                else
                    return "Incorrent username/email";
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                var t = ResetPasswordByEmail(email);

                if (t != null)
                    return "Password reset";
                else
                    return "Incorrent username/email";
            }
            else
                return "Incorrent username/email";
        }

        private bool SendEmail(string recipient, string newPass)
        {
            if (string.IsNullOrWhiteSpace(recipient))
                return false;

            var smtpClient = new SmtpClient()
            {
                Port = EmailPort,
                Credentials = new NetworkCredential(SenderEmail, EmailPassword),
                EnableSsl = true,
                Host = EmailHost
            };

            var mailMsg = new MailMessage
            {
                From = new MailAddress(SenderEmail),
                Subject = "Password Reset",
                Body = "This is an automated message to notify you that your password has been reset to: " + newPass + ". Please log in and change it immediately.",
                IsBodyHtml = true,
            };

            mailMsg.To.Add(recipient);
            smtpClient.Send(mailMsg);
            return true;
        }

        private string NewPassword(int userId)
        {
                LoginCredentials lc = _context.LoginCredentials.Where(x => x.UserId == userId).FirstOrDefault();

            if (lc == null)
                    return "No user found for this id";

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!$&?";
                var stringChars = new char[12];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                lc.PasswordHash = finalString;
                _context.Update(lc);
                string s = _context.LoginCredentials.Where(x => x.UserId == userId).FirstOrDefault().PasswordHash;

            if (s == finalString)
                    return s;
                else
                    return "Something went wrong, please try again or contact an admin.";
        }

        private LoginCredentials ResetPasswordByUsername(string username)
        {
            LoginCredentials lc = _context.LoginCredentials.Where(x => x.Username == username).FirstOrDefault();

            if (lc == null)
                    return null;

            User u = _context.Users.Where(x => x.UserId == lc.UserId).FirstOrDefault();

            if (lc != null && u != null)
                {
                    string s = NewPassword(lc.UserId);

                    if (s == "No user found for this id" || s == "Something went wrong, please try again or contact an admin.")
                        return null;

                    bool c = SendEmail(u.Email, s);

                    if (c)
                        return lc;
                    else
                        return null;
                }
                else
                    return null;
        }
        private LoginCredentials ResetPasswordByEmail(string email)
        {
           
            User u = _context.Users.Where(x => x.Email == email).FirstOrDefault();

            if (u != null)
                {
                    LoginCredentials lc = _context.LoginCredentials.Where(x => x.UserId == u.UserId).FirstOrDefault();

                string s = NewPassword(lc.UserId);

                    if (s == "No user found for this id" || s == "Something went wrong, please try again or contact an admin.")
                        return null;

                    bool c = SendEmail(u.Email, s);

                    if (c)
                        return lc;
                    else
                        return null;
                }
                else
                    return null;
        }
    }
}

