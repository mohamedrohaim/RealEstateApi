using BusinessLayer.Iservices;
using DataAccessLayer;
using DataAccessLayer.DTOs.Unit;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
	public class ScheduleAppointmentSercice : IScheduleAppointmentSercice
	{
		public readonly IUnitOfWork _unitOfWork;
		public ScheduleAppointmentSercice(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public List<ScheduleAppointment> getUserScheduleAppointments(string userId) {
			var ScheduleAppointments =  _unitOfWork.Select<ScheduleAppointment>(x => x.UserId == userId).ToList();
			return ScheduleAppointments;	

		}
		public async Task createAppointment(ScheduleAppointment appointment)
		{

			await _unitOfWork.Add(appointment);
			await _unitOfWork.SaveAsync();
		}

		public void ApproveScheduleAppointment(int id)
		{
			var scheduleAppointment =_unitOfWork.Select<ScheduleAppointment>(x=>x.Id==id).FirstOrDefault();
			if (scheduleAppointment != null) {
				scheduleAppointment.isApproved = true;
				_unitOfWork.SaveAsync().Wait();
			}
			//test
		}
	}
}
