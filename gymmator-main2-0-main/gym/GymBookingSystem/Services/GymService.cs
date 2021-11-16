using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;

namespace GymBookingSystem.Services
{
    public class GymService : IGymService
    {
        private readonly GymContext _context;
        public GymService(GymContext context)
        {
            _context = context;
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
        public Gym DeleteGym(int GymId)
        {
            List<Booking> b = _context.Bookings.Where(x => x.GymId == GymId).ToList();
            List<TrainingClass> t = _context.TrainingClasses.Where(x => x.GymId == GymId).ToList();
            Gym g = _context.Gyms.Where(x => x.GymId == GymId).FirstOrDefault();

            if (t == null)
            {
                return null;
            }
            b.ForEach(x => _context.Bookings.Remove(x));
            t.ForEach(x => _context.TrainingClasses.Remove(x));
            _context.Gyms.Remove(g);

            _context.SaveChanges();
            return g;
        }
    }
}
