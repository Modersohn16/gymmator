using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBookingSystem.Controllers
{
    public class ControllerBase2 : ControllerBase
    {
        protected int GetUserId()
        {
            return int.Parse(this.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);
        }
    }
}
