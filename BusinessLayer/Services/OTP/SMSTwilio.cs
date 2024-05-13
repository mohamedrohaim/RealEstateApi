using BusinessLayer.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Utilities;

namespace BusinessLayer.Services.OTP
{
	public class SMSTwilio : ISMSTwilio
	{
		public MessageResource sendSms(string phoneNumber, string message)
		{
			TwilioClient.Init(TwilioSettings.AccountSID, TwilioSettings.AuthToken);
			var result = MessageResource.Create(
				body: message,
				from: new Twilio.Types.PhoneNumber(TwilioSettings.PhoneNumber),
				to:phoneNumber
				);
			return	result; 
		}
	}
}
