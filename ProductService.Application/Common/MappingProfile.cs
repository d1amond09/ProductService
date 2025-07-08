using AutoMapper;
using ProductService.Application.Common.DTOs;
using ProductService.Domain.Products;

namespace ProductService.Application.Common;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Product, ProductDetailsDto>();
		CreateMap<Product, ProductSummaryDto>();
		CreateMap<ProductForUpdateDto, Product>();
		CreateMap<ProductForCreationDto, Product>();
	}
}
