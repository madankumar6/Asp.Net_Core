using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Shopping.Api.Data;
using Shopping.Api.Models;

namespace Shopping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductContext productContext;
        public ProductController(ILogger<ProductController> logger, ProductContext productContext)
        {
            _logger = logger;
            this.productContext = productContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await productContext.Products.Find(p => true).ToListAsync();
        }
    }
}
