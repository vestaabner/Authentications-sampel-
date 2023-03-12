using System;
using System.Text;
using BackOffice.Application.Abstraction;
using BackOffice.Domain.Entities;
using BackOffice.Infrastructure.Context;
using BackOffice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BackOffice.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<IdentityDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    ClockSkew = TimeSpan.Zero,
                };

                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(context.Exception.Message));
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();
            services.AddScoped<IUserServiceRepository, UserServiceRepository>();



            services
                .AddSwaggerGen(c =>
                {
                    var securityScheme = new OpenApiSecurityScheme
                    {
                        Description = "Bearer {token}",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    };
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Hooshmand.BackOffice.Api",
                        Version = "v1"
                    });
                    c.AddSecurityDefinition("Bearer", securityScheme);
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new [] {"Bearer"} }
                });
                });
        }
    }
}

