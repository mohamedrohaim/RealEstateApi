using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public string Governate { get; set; }
        public string? Image { get; set; }
       // public string? OTP { get; set; }

        //public ICollection<Unit> Units = new List<Unit>();
        public ICollection<Favorite> Favorites = new List<Favorite>();
        public ICollection<Comment> Comments = new List<Comment>();
        public ICollection<ScheduleAppointment> CommeScheduleAppointmentnts = new List<ScheduleAppointment>();
    }
}
