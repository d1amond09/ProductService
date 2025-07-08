using MediatR;
using ProductService.Application.Common.DTOs;

namespace ProductService.Application.UseCases.Products.AddProduct;

public sealed record AddProductCommand(ProductForCreationDto Product) : IRequest<Guid>;
