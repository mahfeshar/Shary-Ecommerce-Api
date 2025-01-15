using Shary.API.Helpers;
using Shary.Core;
using Shary.Core.Repositories.Contract;
using Shary.Core.Services.Contract;
using Shary.Repository;
using Shary.Service;

namespace Shary.API.Extensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

        services.AddScoped(typeof(IOrderService), typeof(OrderService));
        services.AddScoped(typeof(IProductService), typeof(ProductService));

        services.AddAutoMapper(typeof(MappingProfiles));

        return services;
    }
}
