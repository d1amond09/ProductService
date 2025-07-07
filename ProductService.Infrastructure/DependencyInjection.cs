using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Common.Interfaces;
using ProductService.Infrastructure.Common.Persistence;
using ProductService.Infrastructure.Products.Persistence;

namespace ProductService.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration config)
	{
		services
			.AddPersistence(config);

		return services;
	}

	private static IServiceCollection AddPersistence(
		this IServiceCollection services, 
		IConfiguration configuration)
	{
		string connectionString = configuration.GetConnectionString("Default")
			?? Environment.GetEnvironmentVariable("DEFAULT_DB_CONNECTION")
			?? string.Empty;

		services.AddDbContext<AppDbContext>(opts =>
			opts.UseNpgsql(connectionString, b =>
			{
				b.MigrationsAssembly("ProductService.Infrastructure");
				b.EnableRetryOnFailure();
			})
		);

		services.AddScoped<IProductRepository, ProductRepository>();

		return services;
	}
}
