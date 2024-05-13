
using DataAccessLayer.Models;
using Twilio.Rest.Api.V2010.Account;

namespace BusinessLayer.Iservices
{

	public interface IOtpSender
	{
		Task<bool> SendOtpAsync(string reciverEmail, string otpCode);
		

	}
}
