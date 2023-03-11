using BackOffice.Application;
using BackOffice.Application.CQRS.Queries;
using BackOffice.Application.Dtos;
using BackOffice.Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddTransient<IRequestHandler<GetProductsQuery, GetProductsQueryResponseDto>, GetProductsQueryHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

