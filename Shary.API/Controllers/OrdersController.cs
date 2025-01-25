
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shary.API.Dtos;
using Shary.API.Errors;
using Shary.Core.Entities.Order_Aggregate;
using Shary.Core.Services.Contract;
using System.Security.Claims;

namespace Shary.API.Controllers;

[Authorize]
public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }
    [ProducesResponseType(typeof(Order), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [HttpPost]
    public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
    {
        var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
        var address = _mapper.Map<AddressDto, Address>(orderDto.shipToAddress);
        var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);
        if (order is null)
            return BadRequest(new ApiResponse(400));
        return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
    }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var orders = await _orderService.GetOrdersForUserAsync(email);
        return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderForUser(int id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var order = await _orderService.GetOrderByIdForUserAsync(id, email);
        if (order is null) return NotFound(new ApiResponse(404));
        return Ok(_mapper.Map<OrderToReturnDto>(order));
    }
    [HttpGet("deliveryMethods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
    {
        var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
        return Ok(deliveryMethods);
    }
}
