
using Shary.Core;
using Shary.Core.Entities;
using Shary.Core.Entities.Order_Aggregate;
using Shary.Core.Repositories.Contract;
using Shary.Core.Services.Contract;

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
                var product = await productsRepository.GetAsync(item.Id);
                var productItemOrdered = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }
        }

        var subtotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);

        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);

        var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subtotal);
        await _unitOfWork.Repository<Order>().AddAsync(order);

        var result = await _unitOfWork.CompleteAsync();
        if (result <= 0) return null;
        return order;
    }

    public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        throw new NotImplementedException();
    }
}
