using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tamweely.Application.Interfaces;
using Tamweely.Domain.Entities;
using Tamweely.Infrastructure.Data;
using Tamweely.Infrastructure.Repositories;
using Tamweely.Infrastructure.Services;

namespace Tamweely.Infrastructure.Extensions;

public static class InfrastructureCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TamweelyDbContext>(oprions =>
        {
            oprions.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<IGenericRepository<AddressEntry>, GenericRepository<AddressEntry>>();
        services.AddScoped<IGenericRepository<Department>, GenericRepository<Department>>();
        services.AddScoped<IGenericRepository<JobTitle>, GenericRepository<JobTitle>>();
        services.AddScoped<IAddressBookService, AddressBookService>();
        services.AddScoped<IAuthService, AuthService>();
    }
}
