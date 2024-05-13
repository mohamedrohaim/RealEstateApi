using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccessLayer.DTOs.Acccount
{
	public class RegisterDTO
	{

		[Required(ErrorMessage ="name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage ="email is required")]
		[EmailAddress(ErrorMessage ="invalid Email ")]
		public string Email { get; set; }
		[Required(ErrorMessage ="governate is required")]
		public string Governate { get; set; }

		[Required(ErrorMessage ="password is required")]
		[DataType(DataType.Password)]
		[MinLength(8,ErrorMessage ="Password must be at least 8 character")]
		public string Password { get; set; }
		
		public string PhoneNumber { get; set; }



		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		public RegisterDTO(string name,string email, string password, string confirmPassword)
		{
			Name = name;
			Email = email;
			Password = password;
			ConfirmPassword = confirmPassword;
		}
	}
}
