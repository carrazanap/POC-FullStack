using Application.Repositories;
using Domain.Others.Utils;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using static Domain.Enums.Enums;

namespace Infrastructure.Factories
{
    internal static class DatabaseFactory
    {
        public static void CreateDataBase(this IServiceCollection services, string dbType, IConfiguration configuration)
        {
            switch (dbType.ToEnum<DatabaseType>())
            {
                case DatabaseType.MYSQL:
                case DatabaseType.MARIADB:
                case DatabaseType.SQLSERVER:
                    services.AddSqlServerRepositories(configuration);
                    break;
                case DatabaseType.MONGODB:
                    services.AddMongoDbRepositories(configuration);
                    break;
                default:
                    throw new NotSupportedException(InfrastructureConstants.DATABASE_TYPE_NOT_SUPPORTED);
            }
        }

        private static IServiceCollection AddSqlServerRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Repositories.Sql.StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
            }, ServiceLifetime.Scoped);

            // Verificar si puede usar las tablas
            var context = services.BuildServiceProvider().GetRequiredService<Repositories.Sql.StoreDbContext>();
            try
            {
                // Intentar acceder a la tabla principal
                context.Automovil.Take(1).ToList();
                // Si llega aquí, las tablas existen y funcionan → preservar datos
                Console.WriteLine("Base de datos funcional encontrada, preservando datos");
            }
            catch (Exception)
            {
                // Si falla el acceso a las tablas → recrear
                Console.WriteLine("Recreando base de datos...");
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            /* Sql Repositories */
            services.AddTransient<IDummyEntityRepository, Repositories.Sql.DummyEntityRepository>();
            services.AddTransient<IAlumnoRepository, Repositories.Sql.AlumnoRepository>();
            services.AddTransient<IAutomovilRepository, Repositories.Sql.AutomovilRepository>();

            return services;
        }

        private static IServiceCollection AddMongoDbRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            ConventionRegistry.Register("Camel Case", new ConventionPack { new CamelCaseElementNameConvention() }, _ => true);

            Repositories.Mongo.StoreDbContext db = new(configuration.GetConnectionString("MongoConnection") ?? throw new NullReferenceException());
            services.AddSingleton(typeof(Repositories.Mongo.StoreDbContext), db);

            /* MongoDb Repositories */
            services.AddTransient<IDummyEntityRepository, Repositories.Mongo.DummyEntityRepository>();

            return services;
        }
    }
}
