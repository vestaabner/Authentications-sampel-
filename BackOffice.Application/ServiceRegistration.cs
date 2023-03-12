using BackOffice.Application.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BackOffice.Application;

public static class Application
{

    public static void AddApplicationServices(this IServiceCollection services)
    {

        //services.AddValidatorsFromAssemblyContaining<UserValidator>();
        //services.AddValidatorsFromAssemblyContaining<ProductValidator>();
        //services.AddAutoMapper(Assembly.GetExecutingAssembly());

        

    }
}

