using AutoMapper;
using MediatR;
using ProductService.Application.Common.DTOs;
using ProductService.Application.Common.Interfaces;
using ProductService.Application.Common.RequestFeatures;

namespace ProductService.Application.UseCases.Products.GetProducts;

public class GetUsersHandler(IProductRepository productRep, IMapper mapper) 
	: IRequestHandler<GetProductsQuery, PagedList<ProductSummaryDto>>
{
	public async Task<PagedList<ProductSummaryDto>> Handle(GetProductsQuery request, CancellationToken ct)
	{
		var pagedProducts = await productRep.GetAllAsync(request.ProductParams, request.TrackChanges, ct);

		var productDtos = mapper.Map<List<ProductSummaryDto>>(pagedProducts.Items);

		return new PagedList<ProductSummaryDto>(productDtos, pagedProducts.MetaData);
	}
}