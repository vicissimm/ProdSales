using Domain.Interfaces;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.DataContext;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ProdSalesContext>();
            services.AddScoped<IProdSalesDataContext,  ProdSalesContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();


            services.AddDbContext<ProdSalesContext>(options =>
            {
                options.UseNpgsql("Host=127.0.0.1;Password=vv;Persist Security Info=True;Username=postgres;Database=postgres");
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            return services;
        }   
    }
}
