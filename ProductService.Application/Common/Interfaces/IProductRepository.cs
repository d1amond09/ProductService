using ProductService.Application.Common.RequestFeatures;
using ProductService.Application.Common.RequestFeatures.ModelParameters;
using ProductService.Domain.Products;

namespace ProductService.Application.Common.Interfaces;

public interface IProductRepository
{
	Task<Product?> GetByIdAsync(Guid id, bool trackChanges, CancellationToken ct);
	Task<PagedList<Product>> GetAllAsync(ProductParameters productParams, bool trackChanges, CancellationToken ct);
	Task<PagedList<Product>> GetAllByUserIdAsync(Guid userId, ProductParameters productParams, bool trackChanges, CancellationToken ct);
}
