using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shary.API.Dtos;
using Shary.API.Errors;
using Shary.API.Helpers;
using Shary.Core.Entities;
using Shary.Core.Services.Contract;
using Shary.Core.Specifications;

namespace Shary.API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
    {
        IReadOnlyList<Product>? products = await _productService.GetProductsAsync(specParams);
        var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
        
        var count = await _productService.GetCountAsync(specParams);

        if (products == null || !products.Any())
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, count, data));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        Product? product = await _productService.GetProductAsync(id);
        if (product == null)
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
    {
        IReadOnlyList<ProductBrand>? brands = await _productService.GetBrandsAsync();
        if (brands == null || !brands.Any())
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok(brands);
    }
    [HttpGet("categories")]
    public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
    {
        IReadOnlyList<ProductCategory>? categories = await _productService.GetCategoriesAsync();
        if (categories == null || !categories.Any())
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok(categories);
    }
}
