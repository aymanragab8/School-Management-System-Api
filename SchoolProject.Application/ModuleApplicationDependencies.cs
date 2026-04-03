using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SchoolProject.Application
{
    public static class ModuleApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ModuleApplicationDependencies).Assembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ModuleApplicationDependencies).Assembly));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
