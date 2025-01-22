using Microsoft.AspNetCore.Mvc;
using Shary.API.Errors;
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

        //services.AddAutoMapper(typeof(MappingProfiles));

        services.AddAutoMapper(m => m.AddProfile<MappingProfiles>());
        services.AddScoped<ProductPictureUrlResolver>();

        services.Configure<ApiBehaviorOptions>(options =>
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = errors
                });
            });

        return services;
    }
}
