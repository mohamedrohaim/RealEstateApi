using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Acccount
{
	public class ConfirmEmailDTO
	{
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email address.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "OTP code is required.")]
		[StringLength(6, MinimumLength = 6, ErrorMessage = "OTP code must be 6 characters long.")]
		public string OTPCode { get; set; }

		[Required(ErrorMessage = "Email confirmation token is required.")]
		public string EmailConfirmationToken { get; set; }

	}
}
