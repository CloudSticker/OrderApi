namespace OrdersApi.Dtos.AddNewOrder;

public class CreateOrderRequest
{
    public IEnumerable<long> ItemsIds { get; set; } = null!;
    public string Address { get; set; } = null!;
}