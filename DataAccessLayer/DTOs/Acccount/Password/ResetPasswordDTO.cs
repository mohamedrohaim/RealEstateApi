using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccessLayer.DTOs.Acccount.Password
{
	public  class ResetPasswordDTO
	{
		[EmailAddress(ErrorMessage = "invalid email")]

		[Required(ErrorMessage ="Email is required")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Old password is required.")]
		[DataType(DataType.Password)]
		[Display(Name = "Old Password")]
		public string? OldPassword { get; set; }

		[Required(ErrorMessage = "New password is required.")]
		[MinLength(8,ErrorMessage ="Password is too short it must at leas 8 char")]
		[DataType(DataType.Password)]
		[Display(Name = "New Password")]
		public string? NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm New Password")]
		[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
		public string? ConfirmNewPassword { get; set; }
	}
}
