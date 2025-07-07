using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace ProductService.API;

public static class DependencyInjection
{
	public static IServiceCollection AddPresentation(
		this IServiceCollection services,
		IConfiguration config)
	{
		services.AddSwagger();
		services.AddCors(options =>
		{
			options.AddDefaultPolicy(builder => 
				builder.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod()
					.WithExposedHeaders("X-Pagination"));
		});

		services.AddControllers()
			.AddJsonOptions(opts => 
				opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

		services.AddEndpointsApiExplorer();

		return services;
	}

	public static IServiceCollection AddSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(s =>
		{
			s.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Product Service API",
				Version = "v1"
			});
			s.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

			s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Place to add JWT with Bearer",
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer"
			});
			s.AddSecurityRequirement(new OpenApiSecurityRequirement() { {
				new OpenApiSecurityScheme {
					Reference = new OpenApiReference {
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					},
					Name = "Bearer",
				},
				new List<string>()
			} });
		});

		return services;
	}

}
