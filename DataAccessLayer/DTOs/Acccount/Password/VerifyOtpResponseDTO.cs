using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Acccount.Password
{
	public class VerifyOtpResponseDTO
	{
		public string? Message { get; set; }
		public string? ResetPasswordToken { get; set; }
		public string? Email { get; set; }
	}
}
