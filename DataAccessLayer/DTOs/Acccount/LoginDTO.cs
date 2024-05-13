using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccessLayer.DTOs.Acccount
{
	public class LoginDTO
	{
		[Required(ErrorMessage ="emails is required")]
		[EmailAddress(ErrorMessage ="invalid email")]
		public string Email { get; set; }

		[Required(ErrorMessage ="password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }





		public LoginDTO(string email, string password)
		{
			Email = email;
			Password = password;
		}
	}
}
