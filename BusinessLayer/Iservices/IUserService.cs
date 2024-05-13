using DataAccessLayer.DTOs.Acccount;
using DataAccessLayer.DTOs.Acccount.Password;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Iservices
{
    public interface IUserService
    {
        Task<object> RegisterUserAsync(RegisterDTO model);
        Task<object> LoginUserAsync(LoginDTO model);
        Task<object> ConfirmEmailAsync(ConfirmEmailDTO model);
        Task<object> SendConfirmEmailTokenAsync(string email);
        Task<object> ResetPasswordAsync(ResetPasswordDTO model);
        Task<object> SendOtpForForgetPasswordResetAsync(SendOtpForForgetPasswordResetDTO model);
        Task<object> VerifyOtpAndEmailAsync(VerifyOtpDTO model);
        Task<object> ResetForgetPasswordWithOtpAsync(ForgetPasswordResetDTO model);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(string userId);
        Task AddToRoleAsync(User user, string role);
    }
}
