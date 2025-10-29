using System.ComponentModel.DataAnnotations;

namespace Marketplace.Api.Models
{
    public class CartItem
    {
        [Key] // 👈 esto marca la clave primaria
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Relación con Product (opcional)
        public Product? Product { get; set; }
    }
}
