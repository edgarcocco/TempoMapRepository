﻿using Microsoft.AspNetCore.Identity;
using TempoMapRepository.Models.Identity;

namespace TempoMapRepository.Data.Config
{
    public static class AuthConfig
    {

        internal static class AdminAccount
        {
            public static string Username { get; set; } = "admin";
            public static string Password { get; set; } = "TenDigitPassword";
            public static string Role { get; set; } = "Administrator";
        }
        public static async Task ConfigAdmin(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetService<UserManager<User>>();
            var username = AdminAccount.Username;
            var password = AdminAccount.Password;
            var role = AdminAccount.Role;

            if (await roleManager.FindByNameAsync(role) == null)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (result.Succeeded)
                    Console.WriteLine($"{username} account added to Db");
                else
                    Console.WriteLine($"{username} is already created in Db");
            }

            if (await userManager.FindByNameAsync(username) == null)
            {
                var user = new User
                {
                    UserName = username,
                    FirstName = "root",
                    LastName = "default:admin:account"
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                    Console.WriteLine($"{username} added to Db");
                }
                else
                    Console.WriteLine($"{username} already added to Db");
            }

        }
    }
}
