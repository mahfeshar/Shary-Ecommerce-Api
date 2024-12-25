using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shary.API.Dtos;
using Shary.Core.Entities;
using Shary.Core.Repositories.Contract;

namespace Shary.API.Controllers;

public class BasketController : BaseApiController
{
    private readonly IBasketRepository _basketRepo;
    private readonly IMapper _mapper;

    public BasketController(IBasketRepository basketRepo, IMapper mapper)
    {
        _basketRepo = basketRepo;
        _mapper = mapper;
    }
    [HttpGet("{basketId}")]
    public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
    {
        CustomerBasket? basket = await _basketRepo.GetBasketAsync(basketId);
        return Ok(basket ?? new CustomerBasket(basketId));
    }
    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basketDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        CustomerBasket? customerBasket = _mapper.Map<CustomerBasket>(basketDto);
        CustomerBasket? createdOrUpdatedBasket = await _basketRepo.UpdateBasketAsync(customerBasket);
        if (createdOrUpdatedBasket == null)
            return BadRequest();
        return Ok(createdOrUpdatedBasket);
    }
    [HttpDelete]
    public async Task<ActionResult> DeleteBasket(string id)
    {
        await _basketRepo.DeleteBasketAsync(id);
        return NoContent();
    }
}
