using Dapper;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.BusinessLayer.Utils;
using OrdersApi.Dtos;
using OrdersApi.Dtos.AddNewOrder;

namespace OrdersApi.BusinessLayer;

public class OrdersRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public OrdersRepository(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

    public async Task<IEnumerable<Item>> GetItems(CancellationToken cancellationToken)
    {
        const string getQuery = $@"SELECT * FROM {SqlConstants.ItemsTable}";
        await using var db = _connectionFactory.CreateDatabase(cancellationToken);
        return await db.Connection.QueryAsync<Item>(db.CreateCommand(getQuery));
    }

    public async Task DeleteItemInOrder(long itemId, long orderId, CancellationToken cancellationToken)
    {
        const string deleteQuery =
            $@"DELETE FROM {SqlConstants.ItemsInOrderTable} WHERE order_id = @orderId AND item_id = @itemId ";
        await using var db = _connectionFactory.CreateDatabase(cancellationToken);
        await db.Connection.QueryAsync(db.CreateCommand(deleteQuery, new
        {
            orderId = orderId,
            itemId = itemId
        }));
    }

    public async Task<long> CreateOderWithItems(CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        const string insertItemsQuery =
            $@"INSERT INTO {SqlConstants.ItemsInOrderTable} 
                    (order_id, item_id)
            VALUES (@OrderId, @ItemId)";
        
        const string insertOrderQuery = 
            $@"INSERT INTO {SqlConstants.OrdersTable} 
                    (address) 
            VALUES (@Address)
            RETURNING id";

        await using var db = _connectionFactory.CreateDatabase(cancellationToken);
        var id = await db.Connection.QueryFirstOrDefaultAsync<long>(insertOrderQuery, new
        {
             request.Address
        });
        
        await db.Connection.ExecuteAsync(insertItemsQuery, request.ItemsIds.Distinct().Select(x => new
        {
            OrderId = id,
            ItemId = x
        }));
        return id;
    }
}