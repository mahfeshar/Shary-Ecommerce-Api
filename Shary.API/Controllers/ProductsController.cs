using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shary.API.Dtos;
using Shary.API.Errors;
using Shary.API.Helpers;
using Shary.Core.Entities;
using Shary.Core.Repositories.Contract;
using Shary.Core.Specifications;
using Shary.Core.Specifications.ProductSpecs;

namespace Shary.API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductBrand> _brandsRepo;
    private readonly IGenericRepository<ProductCategory> _categoriesRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productsRepo,
                              IGenericRepository<ProductBrand> brandsRepo,
                              IGenericRepository<ProductCategory> categoriesRepo,
                              IMapper mapper)
    {
        _productsRepo = productsRepo;
        _brandsRepo = brandsRepo;
        _categoriesRepo = categoriesRepo;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
    {
        ProductWithCategoryAndBrandSpecifications? spec = new (specParams);
        IReadOnlyList<Product>? products = await _productsRepo.GetAllWithSpecAsync(spec);
        var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
        var countSpec = new ProductsWithFiltrationForCountSpec(specParams);
        var count = await _productsRepo.GetCountWithSpecAsync(countSpec);
        if (products == null || !products.Any())
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, count, data));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        ProductWithCategoryAndBrandSpecifications? spec = new ProductWithCategoryAndBrandSpecifications(id);
        Product? product = await _productsRepo.GetWithSpecAsync(spec);
        if (product == null)
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
    {
        IReadOnlyList<ProductBrand>? brands = await _brandsRepo.GetAllAsync();
        if (brands == null || !brands.Any())
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok(brands);
    }
    [HttpGet("categories")]
    public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
    {
        IReadOnlyList<ProductCategory>? categories = await _categoriesRepo.GetAllAsync();
        if (categories == null || !categories.Any())
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok(categories);
    }
}
