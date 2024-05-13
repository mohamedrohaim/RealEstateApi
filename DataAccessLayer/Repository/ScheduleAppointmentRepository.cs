using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
	public class ScheduleAppointmentRepository: Repository<ScheduleAppointment>, IScheduleAppointmentRepository
	{
		private readonly AppDbContext _context;

		public ScheduleAppointmentRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
