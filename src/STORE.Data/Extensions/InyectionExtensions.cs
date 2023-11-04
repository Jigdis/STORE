using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using STORE.Data.Context;
using Microsoft.EntityFrameworkCore;
using STORE.Entities.Interface;
using STORE.Data.Repository;
using Unit = STORE.Data.UnitOfWork;
using System.Reflection;

namespace STORE.Data.Extensions
{
    public static class InyectionExtensions
    {
        public static IServiceCollection AddInjectionPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                 configuration.GetConnectionString("DefaultConnection"),
                 b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, Unit.UnitOfWork>();

            services.AddTransient<IArticuloRepository, ArticuloRepository>();
            services.AddTransient<ITiendaRepository, TiendaRepository>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}
