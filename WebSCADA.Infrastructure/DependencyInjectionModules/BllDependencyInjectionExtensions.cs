using Microsoft.Extensions.DependencyInjection;
using WebSCADA.BLL.Interfaces;
using WebSCADA.BLL.Services;

namespace WebSCADA.Infrastructure.DependencyInjectionModules
{
    public static class BllDependencyInjectionExtensions
    {
        public static IServiceCollection ResolveBllDependencies(this IServiceCollection services)
        {
            services.AddTransient<IModbusService, ModbusService>();
            services.AddTransient<ISchemaService, SchemaService>();
            services.AddTransient<ILogsService, LogsService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
