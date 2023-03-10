using System;
using BackOffice.Domain.Entities;
using BackOffice.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BackOffice.Infrastructure.Seed
{
    public static class SeedData
    {
        public static void SeedingData(this IServiceProvider service)
        {

            var dbContext = service.GetRequiredService<AppDbContext>();

            var manager = service.GetRequiredService<UserManager<User>>();

            var roleManager = service.GetRequiredService<RoleManager<Roles>>();

            dbContext.Database.Migrate();

            if (!manager.Users.Any())
            {
                manager.CreateAsync(new User() { Email = "deneme@gmail.com", UserName = "user1", }, "Password1*");
            }

            if (!roleManager.Roles.Any())
            {

                roleManager.CreateAsync(new Roles { Name = "Admin", NormalizedName = "ADMIN" });

            }
        }

    }
}

