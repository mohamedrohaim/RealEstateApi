using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Unit
{
    public class AllUnitsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string UnitCategoryName { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

    }
}
