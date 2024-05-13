using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.Acccount.Password
{
	public  class VerifyOtpDTO
	{ 
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "OTP is required.")]
		public string? Otp { get; set; }
	
	}
}
