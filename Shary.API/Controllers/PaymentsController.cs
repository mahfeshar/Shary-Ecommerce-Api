
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shary.API.Errors;
using Shary.Core.Entities;
using Shary.Core.Entities.Order_Aggregate;
using Shary.Core.Services.Contract;
using Stripe;

namespace Shary.API.Controllers;

public class PaymentsController : BaseApiController
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentsController> _logger;
    
    private const string endpointSecret = "whsec_ffc467c3daff4992a4cf3f270930c303265872e851930a90b89b67fae22bf9f9";

    public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }

    [Authorize]
    [ProducesResponseType(typeof(CustomerBasket), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
    {
        var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
        if (basket is null) return BadRequest(new ApiResponse(400, "An Error happened with your basket"));
        return Ok(basket);
    }
    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        var stripeEvent = EventUtility.ConstructEvent(
            json,
            Request.Headers["Stripe-Signature"],
            endpointSecret
        );

        var paymentIntent = (PaymentIntent) stripeEvent.Data.Object;

        Order order;

        switch (stripeEvent.Type)
        {
            case EventTypes.PaymentIntentSucceeded:
                order = await _paymentService.UpdateIntentToSucceededOrFailed(paymentIntent.Id, true);
                _logger.LogInformation("Payment is Succeeded.", paymentIntent.Id);
                break;
            case EventTypes.PaymentIntentPaymentFailed:
                order = await _paymentService.UpdateIntentToSucceededOrFailed(paymentIntent.Id, false);
                _logger.LogInformation("Payment is Failed.", paymentIntent.Id);
                break;
        }
        return Ok();
    }
}
