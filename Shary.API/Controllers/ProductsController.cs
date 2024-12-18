using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shary.API.Dtos;
using Shary.Core.Entities;
using Shary.Core.Repositories.Contract;
using Shary.Core.Specifications.ProductSpecs;

namespace Shary.API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productsRepo, IMapper mapper)
    {
        _productsRepo = productsRepo;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        ProductWithCategoryAndBrandSpecifications? spec = new ProductWithCategoryAndBrandSpecifications();
        IEnumerable<Product>? products = await _productsRepo.GetAllWithSpecAsync(spec);
        if (products == null || !products.Any())
        {
            return NotFound(new { message = "Not Found", statusCode = 404});
        }
        return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
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
        return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
    }
}
