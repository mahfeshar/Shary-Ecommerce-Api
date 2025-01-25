
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shary.API.Errors;
using Shary.Core.Entities;
using Shary.Core.Services.Contract;
using Stripe;

namespace Shary.API.Controllers;

[Authorize]
public class PaymentsController : BaseApiController
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [ProducesResponseType(typeof(CustomerBasket), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
    {
        var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
        if (basket is null) return BadRequest(new ApiResponse(400, "An Error happened with your basket"));
        return Ok(basket);
    }
}
