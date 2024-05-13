using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Acccount
{
	public class LoginErrorResponseDTO
	{
		public string? EmailError { get; set; }
		public string? PasswordError { get; set; }
	}
}
