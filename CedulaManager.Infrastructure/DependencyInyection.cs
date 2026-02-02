using CedulaManager.Domain.Interfaces;
using CedulaManager.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CedulaManager.Infrastructure
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var currentAssambly = typeof(DependencyInyection).Assembly;

            services.AddScoped<IAuthService, AuthService>();
            return services;
        }

    }
}
