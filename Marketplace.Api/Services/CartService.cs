using Marketplace.Api.Models;

namespace Marketplace.Api.Services;

public class CartService
{
    private readonly List<CartItem> _items = new();
    private readonly ProductCatalog _catalog;

    public CartService(ProductCatalog catalog)
    {
        _catalog = catalog;
    }

    public IReadOnlyCollection<CartItem> GetItems() => _items;

    public void AddItem(int productId, int quantity = 1)
    {
        if (quantity <= 0) quantity = 1;

        var product = _catalog.GetById(productId);
        if (product is null)
            throw new ArgumentException($"Producto {productId} no existe en el catálogo.");

        var existing = _items.FirstOrDefault(i => i.ProductId == productId);
        if (existing is null)
            _items.Add(new CartItem { ProductId = productId, Quantity = quantity });
        else
            existing.Quantity += quantity;
    }

    public void RemoveItem(int productId)
    {
        var existing = _items.FirstOrDefault(i => i.ProductId == productId);
        if (existing is not null)
            _items.Remove(existing);
    }

    public void Clear() => _items.Clear();

    public decimal GetTotal()
    {
        decimal total = 0;
        foreach (var item in _items)
        {
            var prod = _catalog.GetById(item.ProductId);
            if (prod is not null)
                total += prod.Price * item.Quantity;
        }
        return total;
    }
}
