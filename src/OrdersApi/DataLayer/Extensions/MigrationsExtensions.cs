using FluentMigrator.Runner;

namespace OrdersApi.BusinessLayer.Extensions;

public static class MigrationsExtensions
{
    public static IApplicationBuilder Migrate(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
        runner!.ListMigrations();
        runner.MigrateUp();
        return app;
    }
}