using AutoMapper;
using Shary.Core.Entities;

namespace Shary.API.Dtos.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(D => D.Brand, O => O.MapFrom(S => S.Brand.Name))
            .ForMember(D => D.Category, O => O.MapFrom(S => S.Category.Name))
            .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

    }
}
