using AutoMapper;
using ProductService.Application.Common.Interfaces;
using ProductService.Domain.Products;
using MediatR;

namespace ProductService.Application.UseCases.Products.AddProduct;

public sealed class AddProductHandler(
	IProductRepository productRep, 
	ICurrentUserService currentUserService, 
	IUnitOfWork unitOfWork,
	IMapper mapper) : IRequestHandler<AddProductCommand, Guid>
{
	public async Task<Guid> Handle(AddProductCommand request, CancellationToken ct)
	{
        var productToCreate = mapper.Map<Product>(request.Product);
        productToCreate.CreationDate = DateTime.UtcNow;

		productToCreate.UserId = currentUserService.UserId ?? Guid.Empty;
		productRep.Add(productToCreate);
		await unitOfWork.SaveChangesAsync(ct);

		return productToCreate.Id;
	}
}
