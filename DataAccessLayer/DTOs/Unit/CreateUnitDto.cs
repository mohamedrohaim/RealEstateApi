using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Unit
{
    public  class CreateUnitDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Bedrooms is required")]
        public int Bedrooms { get; set; }

        [Required(ErrorMessage = "Bathrooms is required")]
        public int Bathrooms { get; set; }

        [Required(ErrorMessage = "Area is required")]
        public int Area { get; set; }
        [Required(ErrorMessage="Image is required ")]
        public IFormFile Image { get; set; }

        public int? Level { get; set; }

        [Required(ErrorMessage = "IsRent is required")]
        public bool IsRent { get; set; }

        public bool? Garage { get; set; }

        public bool? Garden { get; set; }

        [Required(ErrorMessage = "DateBuilt is required")]
        public DateTime DateBuilt { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "UnitCategoryId is required")]
        public int UnitCategoryId { get; set; }

        //[Required(ErrorMessage = "UserId is required")]
        //public string UserId { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
    }
}
