using DataAccessLayer.DTOs.Acccount;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IUserRepository
    {
        Task<User?> FindByEmailAsync(string email);
        Task<User?> FindByIdAsync(string userId);
        Task<IdentityResult> CreateAsync(User user, string password);
        Task AddToRoleAsync(User user, string role);
        Task<IList<string>> GetRolesAsync(User user);
        Task<IList<ClaimDTO>> GetClaimsAsync(User user);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(User user);
        Task<IdentityResult> ConfirmEmailAsync(User user, string emailConfirmationToken);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
        Task<JwtSecurityToken> CreateJwtToken(User user);
    }
}
