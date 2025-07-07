using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Common;

namespace ProductService.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
		services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

		return services;
	}
}
