

using BusinessLayer.Iservices;
using DataAccessLayer.Models;
using Twilio.Rest.Api.V2010.Account;
using Utilities.EmailService;

namespace BusinessLayer.Services.OTP
{
	public class EmailOtpSender : IOtpSender
	{

		public async Task<bool> SendOtpAsync(string reciverEmail, string otpCode)
		{
			Email email = new Email()
			{
				Reciver = reciverEmail,
				Subject = "OTP Code for Email Confirmation",
				Body = $"Your OTP code for email confirmation is: {otpCode}",
			};
			EmailSettings.SendEmail(email);

			return true;
		}

		
	}
}
