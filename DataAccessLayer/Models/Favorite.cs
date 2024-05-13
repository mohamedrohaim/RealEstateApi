using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Favorite
    {
        [Key] 
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public int UnitId { get; set; }
        public Unit? Unit { get; set; }
        public User? User { get; set; }
    }
}
