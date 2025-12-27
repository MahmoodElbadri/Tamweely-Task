using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

namespace Tamweely.Application.Extensions;

public static class ApplicationCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ApplicationCollectionExtensions).Assembly);
        services.AddValidatorsFromAssembly(typeof(ApplicationCollectionExtensions).Assembly);
        services.AddFluentValidationAutoValidation();
    }
}
