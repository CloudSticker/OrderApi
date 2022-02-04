using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.BusinessLayer;
using OrdersApi.Dtos.AddNewOrder;

namespace OrdersApi.Controllers;

[ApiController]
public class OrdersController : ControllerBase
{
    private readonly OrdersRepository _repository;
    private readonly HttpCancellationTokenAccessor _cancellationToken;

    public OrdersController(OrdersRepository repository, HttpCancellationTokenAccessor cancellationToken)
    {
        _repository = repository;
        _cancellationToken = cancellationToken;
    }

    [HttpGet("/api/v1/items")]
    public async Task<IActionResult> GetItems() => Ok(await _repository.GetItems(_cancellationToken.Token));

    [HttpPost("/api/v1/order")]
    public async Task<IActionResult> CreateOrderWithItems([FromBody] CreateOrderRequest request)
    {
        return Ok(await _repository.CreateOderWithItems(request, _cancellationToken.Token));
    }

    [HttpDelete("/api/v1/order/{orderId:long}/{itemId}")]
    public async Task<IActionResult> DeleteItemsFromOrder(long itemId, long orderId)
    {
        await _repository.DeleteItemInOrder(itemId, orderId, _cancellationToken.Token);
        return Ok();
    }
}