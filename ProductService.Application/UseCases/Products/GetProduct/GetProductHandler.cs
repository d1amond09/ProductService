using AutoMapper;
using MediatR;
using ProductService.Application.Common.DTOs;
using ProductService.Application.Common.Exceptions;
using ProductService.Application.Common.Interfaces;
using ProductService.Domain.Products;

namespace ProductService.Application.UseCases.Products.GetProduct;

public class GetProductHandler(IMapper mapper, IProductRepository productRep)
	: IRequestHandler<GetProductQuery, ProductDetailsDto>
{
	public async Task<ProductDetailsDto> Handle(GetProductQuery request, CancellationToken ct)
	{
		Product product = await productRep.GetByIdAsync(request.Id, false, ct)
			?? throw new NotFoundException("User not found.");

		var productDto = mapper.Map<ProductDetailsDto>(product);

		return productDto;
	}
}
