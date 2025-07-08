using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductService.Application.Common.Interfaces;
using ProductService.Infrastructure.Common.Configuration;
using ProductService.Infrastructure.Common.Persistence;
using ProductService.Infrastructure.Products.Persistence;
using ProductService.Infrastructure.Services;

namespace ProductService.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration config)
	{
		services
			.AddHttpContextAccessor()
			.AddConfigurations(config)
			.AddServices()
			.AddAuthentication(config)
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
		services.AddScoped<IUnitOfWork>(p => p.GetRequiredService<AppDbContext>());
		return services;
	}

	private static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration config)
	{
		services.Configure<JwtSettings>(config.GetSection(JwtSettings.SectionName));

		return services;
	}

	private static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		services.AddScoped<ICurrentUserService, CurrentUserService>();

		return services;
	}


	private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
	{
		var jwtSettings = new JwtSettings();
		config.Bind(JwtSettings.SectionName, jwtSettings);
		services.AddSingleton(Options.Create(jwtSettings));

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateIssuerSigningKey = true,
				ValidateLifetime = true,
				ValidIssuer = jwtSettings.ValidIssuer,
				ValidAudience = jwtSettings.ValidAudience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
			};
		});

		return services;
	}
}
