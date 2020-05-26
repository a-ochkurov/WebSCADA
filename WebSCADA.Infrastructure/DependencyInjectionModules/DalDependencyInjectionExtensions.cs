using Microsoft.Extensions.DependencyInjection;
using WebSCADA.DAL.Database;
using WebSCADA.DAL.Database.Repositories;
using WebSCADA.DAL.Entities;
using WebSCADA.DAL.Interfaces;
using WebSCADA.DAL.Modbus;

namespace WebSCADA.Infrastructure.DependencyInjectionModules
{
    public static class DalDependencyInjectionExtensions
    {
        public static IServiceCollection ResolveDalDependencies(this IServiceCollection services, string connectionString, string dbName, string ipAddress, int port)
        {
            services.AddScoped(c => new ScadaMongoDbContext(connectionString, dbName));
            services.AddSingleton<IModbusTCPService>(m => new ModbusTCPService(ipAddress, port));
            services.AddTransient<IRepository<Schema>, SchemaRepository>();
            services.AddTransient<IRepository<Log>, LogsRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Role>, RoleRepository>();

            return services;
        }
    }
}
