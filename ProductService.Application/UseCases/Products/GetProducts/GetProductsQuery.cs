using MediatR;
using ProductService.Application.Common.DTOs;
using ProductService.Application.Common.RequestFeatures;
using ProductService.Application.Common.RequestFeatures.ModelParameters;

namespace ProductService.Application.UseCases.Products.GetProducts;

public sealed record GetProductsQuery(ProductParameters ProductParams, bool TrackChanges) :
	IRequest<PagedList<ProductSummaryDto>>;
