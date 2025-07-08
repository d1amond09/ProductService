using MediatR;
using ProductService.Application.Common.DTOs;

namespace ProductService.Application.UseCases.Products.UpdateProduct;

public record UpdateProductCommand(Guid ProductId, ProductForUpdateDto ProductData) : IRequest;
