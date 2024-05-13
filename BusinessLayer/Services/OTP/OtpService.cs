

//using BusinessLayer.Iservices;
//using DataAccessLayer.IRepository;
//using DataAccessLayer.Models;

//namespace BusinessLayer.Services.OTP
//{
//	public class OtpService
//	{
//		private readonly IUserRepository _userRepository;
//		private readonly IOtpSender _otpSender;

		

//		public OtpService(IUserRepository userRepository, IOtpSender otpSender)
//		{
//			_userRepository = userRepository;
//			_otpSender = otpSender;
//		}

//		public async Task<bool> SendOtpCodeAsync(User user)
//		{
//			var otpCode = GenerateOTP();
//			bool saved = await _userRepository.SaveOtpAsync(user, otpCode);

//			if (saved)
//			{
//				await _otpSender.SendOtpAsync(user, otpCode);
//			}

//			return saved;
//		}

//		private string GenerateOTP()
//		{
//			Random random = new Random();
//			int otp = random.Next(100000, 999999);
//			return otp.ToString();
//		}

//	}
//}
