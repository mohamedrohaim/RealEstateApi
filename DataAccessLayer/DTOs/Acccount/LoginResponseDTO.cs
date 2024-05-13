using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Acccount
{
	public class LoginResponseDTO
	{
		public string? userId { get; set; }
		public string? Name { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? Token { get; set; }
		public DateTime? ExpirationTokenDate { get; set; }
		public List<string>? Roles { get; set; }

	}
}
