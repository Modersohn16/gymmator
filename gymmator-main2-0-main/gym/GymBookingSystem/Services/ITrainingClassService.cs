using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBookingSystem.Models;
using GymBookingSystem.Models.DTO;

namespace GymBookingSystem.Services
{
    public interface ITrainingClassService
    {
        TrainingClass GetTrainingClass(int Id);
        List<TrainingClass> GetTrainingClasses(bool onlyAvailable);
        TrainingClassListDto GetTrainingClassesAtGym(int gymId);
        List<TrainingClass> GetTrainingClassesAtDate(DateTime dateTime);
        TrainingClass CreateTrainingClass(TrainingClassDto dto);
        TrainingClass DeleteTrainingClass(int TrainingClassId);
        TrainingClass UpdateTrainingClass(int trainingClassId, TrainingClassDto dto);
    }
}
