using Microsoft.AspNetCore.Mvc;
using Shary.Core.Entities;
using Shary.Core.Repositories.Contract;
using Shary.Core.Specifications.ProductSpecs;

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
        var spec = new ProductWithCategoryAndBrandSpecifications();
        var products = await _productsRepo.GetAllWithSpecAsync(spec);
        if (products == null || !products.Any())
        {
            return NotFound(new { message = "Not Found", statusCode = 404});
        }
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        ProductWithCategoryAndBrandSpecifications? spec = new ProductWithCategoryAndBrandSpecifications(id);
        Product? product = await _productsRepo.GetWithSpecAsync(spec);
        if (product == null)
        {
            return NotFound(new { message = "Not Found", statusCode = 404 });
        }
        return Ok(product);
    }
}
