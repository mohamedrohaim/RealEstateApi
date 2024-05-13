using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class UnitCategory:BaseEntity<int>
    {
        public string Name { get; set; }
        public ICollection<Unit> Units { get; set; }=new List<Unit>();
    }
}
