using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Acccount.Password
{
	public class ForgetPasswordResetDTO
	{
			[Required(ErrorMessage = "Email is required.")]
			[EmailAddress(ErrorMessage = "Invalid email format.")]
			public string Email { get; set; }

			[Required(ErrorMessage = "New password is required.")]
			[DataType(DataType.Password)]
			public string NewPassword { get; set; }

			[Required(ErrorMessage = "Confirm password is required.")]
			[DataType(DataType.Password)]
			[Compare("NewPassword", ErrorMessage = "The new password and confirm password do not match.")]
			public string ConfirmPassword { get; set; }

			[Required(ErrorMessage = "The new password and confirm password do not match.")]
			public string Token { get; set; } 
			[Required(ErrorMessage = "Otp is required")]
			public string Otp { get; set; } 
		}

	
}
