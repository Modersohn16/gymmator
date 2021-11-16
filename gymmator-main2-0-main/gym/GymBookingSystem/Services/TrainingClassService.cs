using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;

namespace GymBookingSystem.Services
{
    public class TrainingClassService : ITrainingClassService
    {
        private readonly GymContext _context;
        public TrainingClassService(GymContext context)
        {
            _context = context;
        }
        public TrainingClass GetTrainingClass(int Id)
        {
            var t = _context.TrainingClasses.Where(x => x.TrainingClassId == Id).FirstOrDefault();

            if (t != null)
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
            List<TrainingClass> t = _context.TrainingClasses.ToList();

            if (t != null)
            {
                return t;
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
        public TrainingClass DeleteTrainingClass(int trainingClassId)
        {
            List<Booking> b = _context.Bookings.Where(x => x.TrainingClassId == trainingClassId).ToList();
            TrainingClass t = _context.TrainingClasses.Where(x => x.TrainingClassId == trainingClassId).FirstOrDefault();

            if (t == null)
            {
                return null;
            }
            _context.TrainingClasses.Remove(t);
            b.ForEach(x => _context.Bookings.Remove(x));
            _context.SaveChanges();
            return t;
        }
        public TrainingClass UpdateTrainingClass(int trainingClassId, TrainingClassDto dto)
        {
            TrainingClass t = _context.TrainingClasses.Where(x => x.TrainingClassId == trainingClassId).FirstOrDefault();
            if (t == null)
                return null;
            t.Name = dto.Name;
            t.GymId = dto.GymId;
            t.MaxPeople = dto.MaxPeople;
            t.TrainerId = dto.TrainerId;
            t.Description = dto.Description;
            t.Start = dto.Start;
            t.End = dto.End;
            _context.Update(t);
            return t;
        }
    }
}
