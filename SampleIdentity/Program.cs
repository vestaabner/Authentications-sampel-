using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SampleIdentity.Data;
using SampleIdentity.Entites;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("TicketingConnection");
if (connectionString is null)
    connectionString = "Server=DESKTOP-E725BPE;Database=AhmadIdentityDb;Integrated Security=true;MultipleActiveResultSets = true; TrustServerCertificate = True;";


//builder.Services.AddDbContext<AppDbContext>(p => p.UseSqlServer(connectionString));

//builder.Services.AddIdentity<User, Role>()
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddDefaultTokenProviders();

var key = Encoding.ASCII.GetBytes("@#MY_BIG_SECRET_KEY@#");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; ;

}).AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var userMachine = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var user = userMachine.GetUserAsync(context.HttpContext.User);
            if (user == null)
                context.Fail("UnAuthorized");


            return Task.CompletedTask;
        }
    };
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();    

app.MapControllers();

app.Run();
