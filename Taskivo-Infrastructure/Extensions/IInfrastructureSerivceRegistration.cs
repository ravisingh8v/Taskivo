using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taskivo_Infrastructure.Authorization;

namespace Taskivo_Infrastructure;
public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Access UseNpgsql safely inside the Infrastructure layer
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IRoleProvider, RoleProvider>();
        services.AddScoped<IPermissionChecker, PermissionChecker>();

        return services;
    }
}