using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;

namespace GymBookingSystem.Services
{
    public interface IGymService
    {
        Gym GetGym(int Id);
        List<Gym> GetGyms();
        Gym CreateGym(GymDto dto);
        Gym DeleteGym(int GymId);
        Gym UpdateGym(int gymId, GymDto dto);
    }
}
