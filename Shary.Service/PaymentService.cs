﻿
using Microsoft.Extensions.Configuration;
using Shary.Core;
using Shary.Core.Entities;
using Shary.Core.Entities.Order_Aggregate;
using Shary.Core.Repositories.Contract;
using Shary.Core.Services.Contract;
using Shary.Core.Specifications.OrderSpecs;
using Stripe;
using Product = Shary.Core.Entities.Product;

namespace Shary.Service;

public class PaymentService : IPaymentService
{
    private readonly IConfiguration _configuration;
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(
        IConfiguration configuration,
        IBasketRepository basketRepo,
        IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _basketRepo = basketRepo;
        _unitOfWork = unitOfWork;
    }
    public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
    {
        StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

        CustomerBasket? basket = await _basketRepo.GetBasketAsync(basketId);
        if (basket is null) return null;

        var shippingPrice = 0m;
        if(basket.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = deliveryMethod.Cost;
            shippingPrice = deliveryMethod.Cost;
        }

        if (basket?.Items.Count() > 0)
        {
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (item.Price != product.Price)
                    item.Price = product.Price;
            }
        }

        PaymentIntentService paymentIntentService = new();
        PaymentIntent paymentIntent;
        if (basket.PaymentIntentId == null)
        {
            var createOptions = new PaymentIntentCreateOptions()
            {
                Amount = (long) basket.Items.Sum(item => (item.Price * 100) * item.Quantity) + (long) shippingPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };
            paymentIntent = await paymentIntentService.CreateAsync(createOptions);

            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;
        }
        else
        {
            var updateOptions = new PaymentIntentUpdateOptions()
            {
                Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)shippingPrice * 100
            };
            await paymentIntentService.UpdateAsync(basket.PaymentIntentId, updateOptions);
        }
        await _basketRepo.UpdateBasketAsync(basket);
        return basket;
    }

    public async Task<Order> UpdateIntentToSucceededOrFailed(string paymentIntentId, bool isSucceeded)
    {
        var spec = new OrderWithPaymentIntentSpecifications(paymentIntentId);
        var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);
        if (isSucceeded)
            order.Status = OrderStatus.PaymentReceived;
        else
            order.Status = OrderStatus.PaymentFailed;

        _unitOfWork.Repository<Order>().Update(order);

        await _unitOfWork.CompleteAsync();

        return order;
    }
}
