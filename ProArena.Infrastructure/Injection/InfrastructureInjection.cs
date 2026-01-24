using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProArena.Application.Interfaces;
using ProArena.Application.Services;
using ProArena.Domain.Interfaces;
using ProArena.Infrastructure.Data.Context;
using ProArena.Infrastructure.Repositories;

namespace ProArena.Infrastructure.Injection
{
    public static class InfrastructureInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProArenaContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("ProArena.Infrastructure")));

            // Registro Repositórios
            services.AddScoped<ICampeonatoRepository, CampeonatoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IJogadorRepository, JogadorRepository>();

            return services;
        }
    }
}
