using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CedulaManager.Application
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var currentAssambly = typeof(DependencyInyection).Assembly;

            services.AddMediatR(config => config.RegisterServicesFromAssembly(currentAssambly));
            services.AddValidatorsFromAssembly(currentAssambly);
            return services;
        }
    }
}
