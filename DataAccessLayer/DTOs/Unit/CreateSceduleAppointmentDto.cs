﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Unit
{
	public class CreateSceduleAppointmentDto
	{
		public DateTime ScheduleDate { get; set; }

		public int UnitId { get; set; }
		public string UserId { get; set; }
		public string WhatsappNumber { get; set; }
		public string Email { get; set; }
		public bool isApproved { get; set; }
	}
}
