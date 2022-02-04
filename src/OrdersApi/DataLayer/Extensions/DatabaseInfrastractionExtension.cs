using System.Reflection;
using FluentMigrator.Runner;
using OrdersApi.BusinessLayer.Utils;

namespace OrdersApi.BusinessLayer.Extensions;

public static class DatabaseInfrastructureExtension
{
    public static IServiceCollection AddDatabaseInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<OrdersRepository>()
            .AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

        return services.AddFluentMigratorCore()
            .ConfigureRunner(x
                => x.AddPostgres()
                    .WithGlobalConnectionString(configuration.GetConnectionString("Postgre"))
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .AddLogging(y => y.AddFluentMigratorConsole());
    }
}