using AutoMapper;
using Shary.API.Dtos;
using Shary.Core.Entities;
using Shary.Core.Entities.Order_Aggregate;

using UserAddress = Shary.Core.Entities.Identity.Address;
using OrderAddress = Shary.Core.Entities.Order_Aggregate.Address;

namespace Shary.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(D => D.Brand, O => O.MapFrom(S => S.Brand.Name))
            .ForMember(D => D.Category, O => O.MapFrom(S => S.Category.Name))
            .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
        
        CreateMap<CustomerBasketDto, CustomerBasket>();
        CreateMap<BasketItemDto, BasketItem>();
        
        CreateMap<AddressDto, OrderAddress>();

        CreateMap<UserAddress, AddressDto>().ReverseMap();
        
        CreateMap<Order, OrderToReturnDto>()
            .ForMember(d => d.DeliveryMethod, O => O.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.DeliveryMethodCost, O => O.MapFrom(s => s.DeliveryMethod.Cost));
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductName, O => O.MapFrom(s => s.Product.ProductName))
            .ForMember(d => d.ProductId, O => O.MapFrom(s => s.Product.ProductId))
            .ForMember(d => d.PictureUrl, O => O.MapFrom(s => s.Product.PictureUrl))
            .ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());
    }
}
