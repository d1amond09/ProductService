using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Infrastructure.Common.Persistence;

public abstract class RepositoryBase<T> where T : class
{
	protected readonly AppDbContext _db;
	protected readonly DbSet<T> _dbSet;

	protected RepositoryBase(AppDbContext db)
	{
		_db = db ?? throw new ArgumentNullException(nameof(db));
		_dbSet = _db.Set<T>();
	}

	protected virtual IQueryable<T> FindAll(bool trackChanges = false) =>
		trackChanges
			? _dbSet
			: _dbSet.AsNoTracking();

	protected virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
		trackChanges
			? _dbSet.Where(expression)
			: _dbSet.Where(expression).AsNoTracking();

	protected virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool trackChanges = false, CancellationToken ct = default) =>
		await FindByCondition(expression, trackChanges)
			.FirstOrDefaultAsync(ct);

	protected virtual async Task<int> CountAsync(Expression<Func<T, bool>>? expression = null, CancellationToken ct = default) =>
		expression == null
			? await _dbSet.CountAsync(ct)
			: await _dbSet.CountAsync(expression, ct);

	public void Add(T entity) => _db.Set<T>().Add(entity);
	public void Update(T entity) => _dbSet.Update(entity);
	public void Remove(T entity) => _dbSet.Remove(entity);
}
