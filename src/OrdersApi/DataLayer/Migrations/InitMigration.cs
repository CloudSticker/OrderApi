using FluentMigrator;
using Microsoft.AspNetCore.Mvc;

namespace OrdersApi.BusinessLayer.Migrations;

[Migration(0)]
public class InitMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table(SqlConstants.ItemsTable)
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("name").AsString(50).NotNullable().Unique()
            .WithColumn("price").AsDecimal();

        Create.Table(SqlConstants.OrdersTable)
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("address").AsString(100).NotNullable()
            .WithColumn("created_at").AsDateTime().WithDefault(SystemMethods.CurrentDateTime);

        Create.Table(SqlConstants.ItemsInOrderTable)
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("order_id").AsInt64().ForeignKey(SqlConstants.OrdersTable, "id")
            .WithColumn("item_id").AsInt64().ForeignKey(SqlConstants.ItemsTable, "id");
    }
}