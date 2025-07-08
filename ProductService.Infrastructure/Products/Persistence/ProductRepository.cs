using Microsoft.EntityFrameworkCore;
using ProductService.Application.Common.Interfaces;
using ProductService.Application.Common.RequestFeatures;
using ProductService.Application.Common.RequestFeatures.ModelParameters;
using ProductService.Domain.Products;
using ProductService.Infrastructure.Common.Persistence;
using ProductService.Infrastructure.Products.Persistence.Extensions;

namespace ProductService.Infrastructure.Products.Persistence;

public class ProductRepository(AppDbContext db) : RepositoryBase<Product>(db), IProductRepository
{
	public Task<Product?> GetByIdAsync(Guid id, bool trackChanges, CancellationToken ct) =>
		FirstOrDefaultAsync(c => c.Id.Equals(id), trackChanges, ct);

	public async Task<PagedList<Product>> GetAllAsync(
		ProductParameters productParams,
		bool trackChanges,
		CancellationToken ct)
	{
		var products = FindAll(trackChanges)
			.FilterProducts(productParams.MinPrice, productParams.MaxPrice)
			.Search(productParams.SearchTerm)
			.Sort(productParams.OrderBy);

		return await PagedList<Product>.ToPagedListAsync(
			products,
			productParams.PageNumber,
			productParams.PageSize,
			ct
		);
	}

	public async Task<PagedList<Product>> GetAllByUserIdAsync(
		Guid userId, 
		ProductParameters productParams, 
		bool trackChanges, 
		CancellationToken ct)
	{
		var products = FindAll(trackChanges)
			.Where(x => x.UserId == userId)
			.FilterProducts(productParams.MinPrice, productParams.MaxPrice)
			.Search(productParams.SearchTerm)
			.Sort(productParams.OrderBy);

		return await PagedList<Product>.ToPagedListAsync(
			products,
			productParams.PageNumber,
			productParams.PageSize,
			ct
		);
	}

	public async Task<Product?> FindByIdToUpdateAsync(Guid id, CancellationToken ct) =>
		await _dbSet
			.IgnoreQueryFilters()
			.FirstOrDefaultAsync(p => p.Id == id, ct);
}
