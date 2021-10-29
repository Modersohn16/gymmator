using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Cryptography
{
    public interface IHasher
    {
        string CreateHash(string password);
        bool ValidatePassword(string password, string correctHash);
    }
}
