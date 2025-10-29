using Marketplace.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductCatalog _catalog;

        public ProductsController(ProductCatalog catalog)
        {
            _catalog = catalog;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_catalog.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _catalog.GetById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
    }
}
