namespace OrdersApi.Dtos;

public class Order
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Address { get; set; } = null!;
}