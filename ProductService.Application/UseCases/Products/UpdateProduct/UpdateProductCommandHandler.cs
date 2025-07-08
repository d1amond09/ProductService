using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProductService.Application.Common.Exceptions;
using ProductService.Application.Common.Interfaces;

namespace ProductService.Application.UseCases.Products.UpdateProduct;

public class UpdateProductCommandHandler(
	IProductRepository productRepository,
	IUnitOfWork unitOfWork,
	ICurrentUserService currentUserService,
	IMapper mapper)
	: IRequestHandler<UpdateProductCommand>
{
	public async Task Handle(UpdateProductCommand request, CancellationToken ct)
	{
		var userId = currentUserService.UserId 
			?? throw new UnauthorizedAccessException();

		var productToUpdate = await productRepository.FindByIdToUpdateAsync(request.ProductId, ct) 
			?? throw new NotFoundException($"Product with ID '{request.ProductId}' not found.");
		
		var isAdmin = currentUserService.IsInRole("Admin");

		if (!isAdmin && productToUpdate.UserId != userId)
			throw new ForbiddenAccessException("You are not authorized to update this product.");

		mapper.Map(request.ProductData, productToUpdate);

		productRepository.Update(productToUpdate);

		await unitOfWork.SaveChangesAsync(ct);
	}
}