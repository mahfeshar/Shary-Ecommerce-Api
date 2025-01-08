using AutoMapper;
using Shary.API.Dtos;
using Shary.Core.Entities;
using Shary.Core.Entities.Order_Aggregate;

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
        CreateMap<AddressDto, Address>();
    }
}
