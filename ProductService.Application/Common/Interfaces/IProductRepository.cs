using ProductService.Domain.Products;

namespace ProductService.Application.Common.Interfaces;

public interface IProductRepository
{
	Task<Product?> GetByIdAsync(Guid id, bool trackChanges, CancellationToken ct);
}
