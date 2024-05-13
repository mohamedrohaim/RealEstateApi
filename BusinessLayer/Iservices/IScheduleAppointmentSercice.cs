using DataAccessLayer.DTOs.Unit;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Iservices
{
	public interface IScheduleAppointmentSercice
	{
		List<ScheduleAppointment> getUserScheduleAppointments(string userId);
		Task createAppointment(ScheduleAppointment appointment);
		void ApproveScheduleAppointment(int id);
	}
}
