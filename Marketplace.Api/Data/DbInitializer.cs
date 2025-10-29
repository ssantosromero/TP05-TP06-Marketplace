using Marketplace.Api.Models;

namespace Marketplace.Api.Data
{
    public static class DbInitializer
    {
        public static void Seed(MarketplaceDbContext context)
        {
            // Si ya hay productos, no hacemos nada
            if (context.Products.Any()) return;

            var products = new List<Product>
            {
                new Product { Name = "Laptop Gamer", Price = 1200 },
                new Product { Name = "Auriculares Bluetooth", Price = 200 },
                new Product { Name = "Smartwatch", Price = 350 },
                new Product { Name = "Monitor Curvo", Price = 800 },
                new Product { Name = "Mouse Inalámbrico", Price = 100 }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
