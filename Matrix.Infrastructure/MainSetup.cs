using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Matrix.Infrastructure
{
    public static class MainSetup
    {
        public static void RegisterDI(this IServiceCollection services, IConfiguration configuration)
        {
            DatabaseSetup.AddDatabaseSetup(services, configuration);
            //RepositoriesSetup.AddRepositorySetup(services);
            //ServicesSetup.AddServicesSetup(services);
        }
    }
}

