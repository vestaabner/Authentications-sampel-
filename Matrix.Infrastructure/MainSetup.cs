using System;
using System.Reflection;
using Matrix.Core.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Matrix.Infrastructure
{
    public static class MainSetup
    {
        public static void RegisterDI(this IServiceCollection services, IConfiguration configuration)
        {
            DatabaseSetup.AddDatabaseSetup(services, configuration);
            ///services.AddMediatR(typeof(LoginInputQuery).GetTypeInfo().Assembly);
            //RepositoriesSetup.AddRepositorySetup(services);
            //ServicesSetup.AddServicesSetup(services);
        }
    }
}

