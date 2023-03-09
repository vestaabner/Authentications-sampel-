using System.Reflection;
using System.Text;
using Matrix.Core.Commands;
using Matrix.Infrastructur;
using Matrix.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthStatus = Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Matrix.Infrastructure.Data.Entities;
using Matrix.Core.Queries;
using Matrix.Core.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterDI(builder.Configuration);
//var assemblies = Infrastructure.Assemblies.Append(Assembly.GetExecutingAssembly()).ToArray();

//builder.Services.AddMediatR(Assembly.GetAssembly(typeof(RegisterInputCommand)));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddMediatR(typeof(LoginHandler).Assembly);
//builder.Services.AddMediatR(cfg => cfg.registerSerivesfromAssembeli   );

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(option =>
               {
                   option.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Appsettings:SecretKey").Value)),
                       ValidateIssuer = false,
                       ValidIssuer = builder.Configuration.GetSection("Appsettings:ValidIssuer").Value,
                       ValidateAudience = false,
                       ValidAudience = builder.Configuration.GetSection("Appsettings:ValidAudience").Value,
                       ValidateLifetime = false
                   };
               });

///builder.Services.AddMediatR((typeof(LoginInputQuery).Assembly));

//builder.Services.AddMediatR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

