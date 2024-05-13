using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Unit
{
	public class FilterUnitsDto
	{
		public string title { get; set; }
		public string governate { get; set; }
		public int? priceFrom { get; set; }
		public int? priceTo { get; set; }
	}
}
