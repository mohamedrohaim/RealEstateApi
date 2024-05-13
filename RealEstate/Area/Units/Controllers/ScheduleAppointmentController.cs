using BusinessLayer.Iservices;
using DataAccessLayer.DTOs.Unit;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Area.Units.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScheduleAppointmentController : ControllerBase
	{
		private readonly IScheduleAppointmentSercice _scheduleAppointmentSercice;

		public ScheduleAppointmentController(IScheduleAppointmentSercice scheduleAppointmentSercice)
		{
			_scheduleAppointmentSercice= scheduleAppointmentSercice;
		}
		[HttpGet("user/{userId}")]
		public ActionResult<List<ScheduleAppointment>> GetUserScheduleAppointments(string userId)
		{
			var scheduleAppointments = _scheduleAppointmentSercice.getUserScheduleAppointments(userId);
			return Ok(scheduleAppointments);
		}

		[HttpPost("create")]
		public async Task<ActionResult> CreateAppointment([FromBody] CreateSceduleAppointmentDto appointmentDto)
		{
			if (ModelState.IsValid)
			{
				var appointment =new ScheduleAppointment() {
					Email= appointmentDto.Email,
					isApproved=false,
					UnitId= appointmentDto.UnitId,
					ScheduleDate= appointmentDto.ScheduleDate,
					UserId=appointmentDto.UserId,
					WhatsappNumber=appointmentDto.WhatsappNumber,	

					};
				await _scheduleAppointmentSercice.createAppointment(appointment);
				return Ok();
			}
			return BadRequest(appointmentDto);
			
		}

		[HttpPost("approve/{id}")]
		public ActionResult ApproveScheduleAppointment(int id)
		{
			try
			{
				_scheduleAppointmentSercice.ApproveScheduleAppointment(id);
					return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex);	
			}
			
		}
	}
}
