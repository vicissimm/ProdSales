using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Domain.Interfaces;

using TodoAppMappingProfile = Application.MappingProfile.Mapper;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceProvider AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TodoAppMappingProfile(provider.CreateScope().ServiceProvider.GetService<IPasswordHasher>()));

            }).CreateMapper());

            services.AddHttpContextAccessor();

            var builder = services.AddConvey()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddCommandHandlers()
                .AddInMemoryCommandDispatcher();

            return builder.Build();    
        }
    }
}
