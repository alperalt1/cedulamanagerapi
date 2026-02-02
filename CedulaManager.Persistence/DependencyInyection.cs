using CedulaManager.Domain.Interfaces;
using CedulaManager.Persistence.Contexts;
using CedulaManager.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CedulaManager.Persistence
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    m=> m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                    ));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            return services;
        }
        
    }
}
