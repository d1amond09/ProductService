using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Common.Interfaces;
using ProductService.Domain.Products;
using ProductService.Infrastructure.Common.Persistence;

namespace ProductService.Infrastructure.Products.Persistence;

public class ProductRepository(AppDbContext db) : RepositoryBase<Product>(db), IProductRepository
{
	public Task<Product?> GetByIdAsync(Guid id, bool trackChanges, CancellationToken ct) =>
		FirstOrDefaultAsync(c => c.Id.Equals(id), trackChanges, ct);
}
