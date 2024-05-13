using System.Net;
using System.Net.Mail;

namespace Utilities.EmailService
{
	public static class EmailSettings
    {
		public static string Email = "realestateappinfo@gmail.com";
		public static string DisplayName = "RealEstate App";
		public static string Password= "vhct cmae mxoc vhht";
		public static string Host = "smtp-mail.outlook.com";
		public static int Port = 587;
		static string smtpAddress = "smtp.gmail.com";
		static int portNumber = 587;
		public static void SendEmail(Email email) {
			var client=new SmtpClient(smtpAddress,portNumber);
			client.EnableSsl= true;
			client.Credentials = new NetworkCredential(Email, Password);
			client.Send(Email, email.Reciver,email.Subject,email.Body);

		}


	}
}
