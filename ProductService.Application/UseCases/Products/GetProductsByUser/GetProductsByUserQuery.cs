using MediatR;
using ProductService.Application.Common.DTOs;
using ProductService.Application.Common.RequestFeatures;
using ProductService.Application.Common.RequestFeatures.ModelParameters;

namespace ProductService.Application.UseCases.Products.GetProductsByUser;

public sealed record GetProductsByUserQuery(Guid? UserIdString, ProductParameters ProductParams, bool TrackChanges) : 
	IRequest<PagedList<ProductSummaryDto>>;

