using DataAccessLayer.DTOs.Acccount.Password;
using DataAccessLayer.DTOs.Acccount;
using DataAccessLayer.DTOs;
using DataAccessLayer.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Iservices;
using DataAccessLayer.Models;
using Utilities;
using BusinessLayer.Services.OTP;
using DataAccessLayer;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOtpService _otpService;
        private readonly IOtpSender _otpSender;
        public UserService(
            IUserRepository userRepository,
            IOtpService otpService,
            IOtpSender otpSender
            )
        {
            _userRepository = userRepository;
            _otpService = otpService;
            _otpSender = otpSender;
        }

        public async Task<object> RegisterUserAsync(RegisterDTO model)
        {
            var responseError = new ErrorDTO();

            try
            {
                if (await _userRepository.FindByEmailAsync(model.Email) != null)
                {
                    responseError.Message = "Email Is Already Exist";
                    return responseError;
                }


                var user = new User
                {
                    Name = model.Name,
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    Governate=model.Governate,
                    PhoneNumber=model.PhoneNumber
                
                };

                var result = await _userRepository.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    string otp=_otpService.GenerateOtp(user.Email);
                    var isOtpSent = await _otpSender.SendOtpAsync(user.Email, otp);

                    if (isOtpSent)
                    {
                        var response = new RegisterResponseDTO
                        {
                            Message ="We Sent Otp Please check your email",
                            Email = model.Email,
                            EmailConfirmationToken = await _userRepository.GenerateEmailConfirmationTokenAsync(user),
                        };
                        return response;
                    }
                }
                else
                {   
                    responseError.Message = "Failed To Register";
                    foreach (var error in result.Errors)
                    {
                        responseError.Errors.Add(error.Description.ToString());
                    }
                    return responseError;
                }
            }
            catch (Exception ex)
            {
                responseError.Message = "Internal server Error";
                responseError.Errors.Add(ex.Message);
            }

            return responseError;
        }

        public async Task<object> LoginUserAsync(LoginDTO model)
        {
            var user = await _userRepository.FindByEmailAsync(model.Email);

            var loginError = new LoginErrorResponseDTO();
            if (user == null)
            {
                loginError.EmailError ="Emaol Not Found";
                return loginError;
            }
            else
            {
                var result = await _userRepository.CheckPasswordAsync(user, model.Password);
                if (result)
                {
                    var roles = await _userRepository.GetRolesAsync(user);
                    var jwtSecurityToken = await _userRepository.CreateJwtToken(user);
                    var response = new LoginResponseDTO()
                    {
                        userId = user.Id,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        UserName = user.UserName,
                        Email = user.Email,
                        Name = user.Name,
                        ExpirationTokenDate = jwtSecurityToken.ValidTo,
                        Roles = roles.ToList(),
                    };
                    return response;
                }
                else
                {
                    loginError.PasswordError = "InnCorrect Password";
                    return loginError;
                }
            }
        }

        public async Task<object> ConfirmEmailAsync(ConfirmEmailDTO model)
        {
            var user = await _userRepository.FindByEmailAsync(model.Email);
            var responseError = new ErrorDTO();
            if (user == null)
            {
                responseError.Message = "Email Not Found";
                return responseError;
            }
            else
            {
                if (_otpService.IsValidOtp(model.Email,model.OTPCode))
                {
                    var result = await _userRepository.ConfirmEmailAsync(user, model.EmailConfirmationToken);
                    if (result.Succeeded)
                    {
                        var jwtSecurityToken = await _userRepository.CreateJwtToken(user);
                        var roles = await _userRepository.GetRolesAsync(user);
                        var response = new LoginResponseDTO()
                        {
                            userId = user.Id,
                            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                            UserName = user.UserName,
                            Email = user.Email,
                            Name = user.Name,
                            ExpirationTokenDate = jwtSecurityToken.ValidTo,
                            Roles = roles.ToList(),
                        };
                        return response;
                    }
                    else
                    {
                        responseError.Message = "Invalid OTP";
                        return responseError;
                    }
                }
                else
                {
                    responseError.Message = "Invalid OTP";
                    return responseError;
                }
            }
        }

        public async Task<object> SendConfirmEmailTokenAsync(string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            var responseError = new ErrorDTO();
            if (user == null)
            {
                responseError.Message = "Email Not Found";
                return responseError;
            }

            IOtpSender sender = new EmailOtpSender();

            var otp=_otpService.GenerateOtp(email);
            var isOtpSent =await _otpSender.SendOtpAsync(email.ToString(), otp);

            if (isOtpSent)
            {
                var response = new RegisterResponseDTO
                {
                    Message = "Confirmation OTP",
                    Email = user.Email,
                    EmailConfirmationToken = await _userRepository.GenerateEmailConfirmationTokenAsync(user),
                };
                return response;
            }
            else
            {
                responseError.Message = "Failed To Send OTP";
                return responseError;
            }
        }

        public async Task<object> ResetPasswordAsync(ResetPasswordDTO model)
        {
            var responseSuccess = new SuccessResponseDTO();
            var responseError = new ErrorDTO();

            try
            {
                var user = await _userRepository.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    responseError.Message ="Email Not Found";
                    return responseError;
                }

                var isOldPasswordCorrect = await _userRepository.CheckPasswordAsync(user, model.OldPassword);

                if (!isOldPasswordCorrect)
                {
                    responseError.Message = "iNCorrect Old Passwod";
                    return responseError;
                }

                var changePasswordResult = await _userRepository.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (changePasswordResult.Succeeded)
                {
                    responseSuccess.Message = "Password changed Successully";
                    return responseSuccess;
                }
                else
                {
                    responseError.Message = "Password Change Failed";
                    foreach (var error in changePasswordResult.Errors)
                    {
                        responseError.Errors.Add(error.Description);
                    }
                    return responseError;
                }
            }
            catch (Exception ex)
            {
                responseError.Message = ex.Message;
                return responseError;
            }
        }

        public async Task<object> SendOtpForForgetPasswordResetAsync(SendOtpForForgetPasswordResetDTO model)
        {
            var responseSuccess = new SuccessResponseDTO();
            var responseError = new ErrorDTO();

            try
            {
                var user = await _userRepository.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    responseError.Message ="Email Not Found";
                    return responseError;
                }
                string otp = _otpService.GenerateOtp(user.Email);
                var isOtpSent = await _otpSender.SendOtpAsync(user.Email, otp);


                if (isOtpSent)
                {
                    responseSuccess.Message = "Check Yor Email For OTP";
                    return responseSuccess;
                }
                else
                {
                    responseError.Message = "Failed To Send OTP";
                    return responseError;
                }
            }
            catch (Exception ex)
            {
                responseError.Message = ex.Message;
                return responseError;
            }
        }

        public async Task<object> VerifyOtpAndEmailAsync(VerifyOtpDTO model)
        {
            var responseSuccess = new VerifyOtpResponseDTO();
            var responseError = new ErrorDTO();

            try
            {
                var user = await _userRepository.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    responseError.Message = "Email Not Found";
                    return responseError;
                }

                if (_otpService.IsValidOtp(model.Email,model.Otp))
                {
                    responseSuccess.Message = "OTP Is Correct";
                    responseSuccess.ResetPasswordToken = await _userRepository.GeneratePasswordResetTokenAsync(user);
                    responseSuccess.Email = user.Email;
                    return responseSuccess;
                }
                else
                {
                    responseError.Message = "Invalid OTP";
                    return responseError;
                }
            }
            catch (Exception ex)
            {
                responseError.Message = ex.Message;
                return responseError;
            }
        }

        public async Task<object> ResetForgetPasswordWithOtpAsync(ForgetPasswordResetDTO model)
        {
            var responseSuccess = new SuccessResponseDTO();
            var responseError = new ErrorDTO();

            try
            {
                var user = await _userRepository.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    responseError.Message = "Email Not Found";
                    return responseError;
                }
                

                    var resetPasswordResult = await _userRepository.ResetPasswordAsync(user, model.Token, model.NewPassword);
                    if (resetPasswordResult.Succeeded)
                    {
                        responseSuccess.Message = "Password Changed Success";
                        return responseSuccess;
                    }
                    else
                    {
                        responseError.Message = "Password Change Failed";
                        foreach (var error in resetPasswordResult.Errors)
                        {
                            responseError.Errors.Add(error.Description);
                        }
                        return responseError;
                    }

            }
            catch (Exception ex)
            {
                responseError.Message = ex.Message;
                return responseError;
            }
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            return user;
        }

        public async Task AddToRoleAsync(User user, string role)
        {
            await _userRepository.AddToRoleAsync(user, role);
        }

        public async Task<User?> GetUserById(string userId)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            return user;


        }
    }
}
