using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Unit: BaseEntity<int>
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Area { get; set; }
        public int? Level { get; set; }
        public bool IsRent { get; set; }
        public bool? Garage { get; set; }
        public bool? Garden { get; set; }
        public DateTime DateBuilt { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string Location { get; set; }
        public int UnitCategoryId { get; set; }
        //[ForeignKey("User")]
        //public string UserId { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
       


        public UnitCategory UnitCategory { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        ///public User User { get; set; }
        public ICollection<Favorite> Favorites = new List<Favorite>();
        public ICollection<ScheduleAppointment> ScheduleAppointment = new List<ScheduleAppointment>();

    }
}
