using MediatR;
using ProductService.Application.Common.DTOs;

namespace ProductService.Application.UseCases.Products.GetProduct;

public sealed record GetProductQuery(Guid Id) : IRequest<ProductDetailsDto>;
