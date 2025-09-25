using System;
using API.Interfaces;
using API.Repositories;
using API.Services;

namespace API.Extensions;

public static class APIExtension
{
    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        // Register repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        // Register services
        services.AddScoped<ILogService, LogService>();
        
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();
        

    }


}
