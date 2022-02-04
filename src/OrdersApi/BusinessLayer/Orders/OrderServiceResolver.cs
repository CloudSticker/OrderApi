using OrdersApi.Dtos.AddNewOrder;
using OrdersApi.Dtos.GetItems;

namespace OrdersApi.BusinessLayer.Orders;

public class OrderServiceResolver : IOrderService
{
    private readonly OrdersRepository _repository;

    public OrderServiceResolver(OrdersRepository repository) => _repository = repository;

    public async Task<CreateOrderResponse> CreateOrderWithItems(CreateOrderRequest request, long id,  CancellationToken cancellationToken)
        => new CreateOrderResponse(await _repository.CreateOderWithItems(request, cancellationToken));

    public async Task<GetItemsResponse> GetItems(CancellationToken cancellationToken)
        => new GetItemsResponse(await _repository.GetItems(cancellationToken));
    
    public async Task DeleteItemsInOrder(long itemId, long orderId, CancellationToken cancellationToken)
        => await _repository.DeleteItemInOrder(itemId, orderId, cancellationToken);
}