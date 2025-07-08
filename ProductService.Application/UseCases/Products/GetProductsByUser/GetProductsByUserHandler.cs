using AutoMapper;
using MediatR;
using ProductService.Application.Common.DTOs;
using ProductService.Application.Common.Interfaces;
using ProductService.Application.Common.RequestFeatures;
using ProductService.Application.UseCases.Products.GetProducts;

namespace ProductService.Application.UseCases.Products.GetProductsByUser;

public class GetProductsByUserHandler(IProductRepository productRep, IMapper mapper) 
	: IRequestHandler<GetProductsByUserQuery, PagedList<ProductSummaryDto>>
{
	public async Task<PagedList<ProductSummaryDto>> Handle(GetProductsByUserQuery request, CancellationToken ct)
	{
		var pagedProducts = await productRep.GetAllByUserIdAsync(request.UserIdString.Value ,request.ProductParams, request.TrackChanges, ct);

		var productDtos = mapper.Map<List<ProductSummaryDto>>(pagedProducts.Items);

		return new PagedList<ProductSummaryDto>(productDtos, pagedProducts.MetaData);
	}
}