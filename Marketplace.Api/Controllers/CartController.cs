using Marketplace.Api.Models;
using Marketplace.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly CartService _cart;

    public CartController(CartService cart)
    {
        _cart = cart;
    }

    [HttpGet]
    public IActionResult GetItems() => Ok(_cart.GetItems());

    public record AddItemDto(int ProductId, int Quantity);

    [HttpPost("add")]
    public IActionResult Add([FromBody] AddItemDto dto)
    {
        _cart.AddItem(dto.ProductId, dto.Quantity);
        return Ok(_cart.GetItems());
    }

    [HttpDelete("{productId:int}")]
    public IActionResult Remove(int productId)
    {
        _cart.RemoveItem(productId);
        return NoContent();
    }

    [HttpDelete("clear")]
    public IActionResult Clear()
    {
        _cart.Clear();
        return NoContent();
    }

    [HttpGet("total")]
    public IActionResult Total() => Ok(new { total = _cart.GetTotal() });
}
