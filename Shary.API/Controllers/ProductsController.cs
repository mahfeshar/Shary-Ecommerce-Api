using Microsoft.AspNetCore.Mvc;
using Shary.Core.Entities;
using Shary.Core.Repositories.Contract;

namespace Shary.API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productsRepo;

    public ProductsController(IGenericRepository<Product> productsRepo)
    {
        _productsRepo = productsRepo;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        IEnumerable<Product>? products = await _productsRepo.GetAllAsync();
        if (products == null || !products.Any())
        {
            return NotFound(new { message = "Not Found", statusCode = 404});
        }
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productsRepo.GetAsync(id);
        if (product == null)
        {
            return NotFound(new { message = "Not Found", statusCode = 404 });
        }
        return Ok(product);
    }
}
