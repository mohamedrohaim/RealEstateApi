using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
	public class ScheduleAppointment
	{
		public int Id { get; set; }
		public DateTime ScheduleDate { get; set; }
		[ForeignKey("User")]
		public int UnitId { get; set; }
		public string UserId { get; set; }

		public string WhatsappNumber { get; set; }
		public string Email { get; set; }
		public bool isApproved { get; set; }


		public User? user { get; set; }	
		public Unit? unit { get; set; }



	}
}
