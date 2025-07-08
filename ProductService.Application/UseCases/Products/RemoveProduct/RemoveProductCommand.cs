using MediatR;

namespace ProductService.Application.UseCases.Products.RemoveProduct;

public record RemoveProductCommand(Guid ProductId) : IRequest;
