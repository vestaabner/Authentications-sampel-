using System;
using System.Text;
using BackOffice.Application.Abstraction;
using BackOffice.Domain.Entities;
using BackOffice.Infrastructure.Context;
using BackOffice.Infrastructure.Repositories;
using BackOffice.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BackOffice.Infrastructure
{
    public static class ServiceRegistration
    {

        public static void AddInfrastructureService(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<AppDbContext>(opt =>

            opt.UseSqlServer(configuration["ConnectionString"])

            );

            service.AddTransient<IUserRepository , UserRepository>();
            service.AddTransient<UserManager<User>>();
            service.AddTransient<RoleManager<Roles>>();
            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            service.AddTransient<AppDbContext>();
            service.AddIdentity<User, Roles>(opt =>
            {
                opt.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<AppDbContext>();

            service.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateAudience = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),

                };
            });
        }

    }
}

