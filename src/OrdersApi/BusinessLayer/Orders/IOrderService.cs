using OrdersApi.Dtos.AddNewOrder;
using OrdersApi.Dtos.GetItems;

namespace OrdersApi.BusinessLayer.Orders;

public interface IOrderService
{
    public Task<CreateOrderResponse> CreateOrderWithItems(CreateOrderRequest request, long id,
        CancellationToken cancellationToken);

    public Task<GetItemsResponse> GetItems(CancellationToken cancellationToken);
    public Task DeleteItemsInOrder(long itemId, long orderId, CancellationToken cancellationToken);
}