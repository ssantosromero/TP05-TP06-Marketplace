using Marketplace.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Api.Services
{
    public class ProductCatalog
    {
        private readonly List<Product> _products = new()
        {
            new() { Id = 1, Name = "Laptop Gamer", Price = 1200.00m },
            new() { Id = 2, Name = "Auriculares Bluetooth", Price = 200.00m },
            new() { Id = 3, Name = "Smartwatch", Price = 350.00m },
            new() { Id = 4, Name = "Monitor Curvo", Price = 800.00m },
            new() { Id = 5, Name = "Mouse Inalámbrico", Price = 100.00m }
        };

        public IEnumerable<Product> GetAll() => _products;

        public Product GetById(int id) => _products.FirstOrDefault(p => p.Id == id);
    }
}
