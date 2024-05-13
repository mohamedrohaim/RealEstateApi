using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Unit
{
    public class UnitDto
    {
        public int Id { get; set; }
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
        public string UnitCategoryName { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }



    }
}
