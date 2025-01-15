
using Shary.Core;
using Shary.Core.Entities;
using Shary.Core.Entities.Order_Aggregate;
using Shary.Core.Repositories.Contract;
using Shary.Core.Services.Contract;
using Shary.Core.Specifications;

namespace Shary.Service;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IBasketRepository basketRepo,
        IUnitOfWork unitOfWork)
    {
        _basketRepo = basketRepo;
        _unitOfWork = unitOfWork;
    }
    public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
    {
        var basket = await _basketRepo.GetBasketAsync(basketId);

        var orderItems = new List<OrderItem>();
        if (basket?.Items?.Count > 0)
        {
            var productsRepository = _unitOfWork.Repository<Product>();
            foreach (var item in basket.Items)
            {
                var product = await productsRepository.GetByIdAsync(item.Id);
                var productItemOrdered = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }
        }

        var subtotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);

        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

        var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subtotal);
        await _unitOfWork.Repository<Order>().AddAsync(order);

        var result = await _unitOfWork.CompleteAsync();
        if (result <= 0) return null;
        return order;
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        var ordersRepo = _unitOfWork.Repository<Order>();
        var spec = new OrderSpecifications(buyerEmail);
        return await ordersRepo.GetAllWithSpecAsync(spec);
    }

    public async Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
    {
        var orderRepo = _unitOfWork.Repository<Order>();
        var orderSpec = new OrderSpecifications(orderId, buyerEmail);
        return await orderRepo.GetWithSpecAsync(orderSpec);
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        var deliveryMethodsRepo = _unitOfWork.Repository<DeliveryMethod>();
        return await deliveryMethodsRepo.GetAllAsync();
    }
}
