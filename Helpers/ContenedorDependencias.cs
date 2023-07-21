#region "Usings"
using CursosLuis.Api.Model;
using CursosLuis.Api.Services;
using CursosLuis.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
#endregion

namespace CursosLuis.Api.Helpers
{
    public static class ContenedorDependencias
    {
        #region "Métodos"
        public static IServiceCollection AgregarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CursoDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("Database"), op =>
                {

                    op.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(60), null);
                });
                o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.AddScoped<IUsuariosService, UsuariosService>();//Se genera la instancia de la clase implementacion del metodo obtener
            services.AddScoped<IBitacoraService, BitacoraService>();//Se genera la instancia de la clase implementacion del metodo obtener
            return services;
        }
        #endregion    
    }
}
