
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shary.API.Dtos;
using Shary.API.Errors;
using Shary.Core.Entities.Order_Aggregate;
using Shary.Core.Services.Contract;

namespace Shary.API.Controllers;

public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
    {
        var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
        var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);
        if (order is null)
            return BadRequest(new ApiResponse(400));
        return Ok(order);
    }
}
