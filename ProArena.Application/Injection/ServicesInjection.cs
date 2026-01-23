using Microsoft.Extensions.DependencyInjection;
using ProArena.Application.Interfaces;
using ProArena.Application.Services;

namespace ProArena.Application.Injection
{
    public static class ServicesInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, string connectionString)
        {
            // Registro Serviços
            services.AddScoped<ICampeonatoService, CampeonatoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
