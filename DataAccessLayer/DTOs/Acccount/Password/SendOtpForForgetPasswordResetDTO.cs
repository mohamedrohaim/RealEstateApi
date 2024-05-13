using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Acccount.Password
{
	public class SendOtpForForgetPasswordResetDTO
	{
		[Required(ErrorMessage = "emails is required")]
		[EmailAddress(ErrorMessage = "invalid email")]
		public string Email { get; set; }
	}
}
