using DataAccessLayer.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using DataAccessLayer.Models;

namespace DataAccessLayer.SeedingData
{
    public class SeedingData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                string[] roles = new string[] { StaticDetails.Admin, StaticDetails.Employee, StaticDetails.User };

                foreach (string role in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(context);

                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        await roleStore.CreateAsync(new IdentityRole(role));
                    }
                }

                var user = new User
                {
                    Name = "Admin",
                    Email = "devwizzard1@gmail.com",
                    NormalizedEmail = "DEVWIZZARD1@GMAIL.COM",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    PhoneNumber = "+111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    Governate="Beni Suief",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var password = new PasswordHasher<User>();
                    var hashed = password.HashPassword(user, "admin123");
                    user.PasswordHash = hashed;

                    var userStore = new UserStore<User>(context);
                    var result = await userStore.CreateAsync(user);
                }

                await AssignRoles(serviceProvider, user.Email, StaticDetails.Admin);
                await context.SaveChangesAsync();
            }
        }

        public static async Task AssignRoles(IServiceProvider services, string email,string role)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var user = await userManager.FindByEmailAsync(email);

                if (user != null)
                {
                   if (!await userManager.IsInRoleAsync(user, role))
                   {
                           var result= await userManager.AddToRoleAsync(user, role);
                   }
                }
                context.SaveChanges();
            }
        }
    }

}
