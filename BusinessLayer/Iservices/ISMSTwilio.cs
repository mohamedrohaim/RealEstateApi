using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace BusinessLayer.Iservices
{
	public interface ISMSTwilio
	{
		MessageResource sendSms(string phoneNumber,string message);
	}
}
