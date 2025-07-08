using ProductService.Application.Common.Exceptions;
using ProductService.Application.Common.Interfaces;
using MediatR;

namespace ProductService.Application.UseCases.Products.RemoveProduct;

public sealed class RemoveProductHandler(
	IProductRepository productRep,
	IUnitOfWork unitOfWork,
	ICurrentUserService currentUserService) : IRequestHandler<RemoveProductCommand>
{
	public async Task Handle(RemoveProductCommand request, CancellationToken ct)
	{
		var userId = currentUserService.UserId 
			?? throw new UnauthorizedAccessException();

		var productToDelete = await productRep.GetByIdAsync(request.ProductId, false, ct)
			?? throw new NotFoundException($"Product with ID '{request.ProductId}' not found.");


		var isAdmin = currentUserService.IsInRole("Admin");

		if (!isAdmin && productToDelete.UserId != userId)
			throw new ForbiddenAccessException("You are not authorized to delete this product.");

		productRep.Remove(productToDelete);
		await unitOfWork.SaveChangesAsync(ct);
	}
}
