﻿using ProductService.Domain.Products;
using System.Linq.Dynamic.Core;
using ProductService.Infrastructure.Common.Persistence.Utility;

namespace ProductService.Infrastructure.Products.Persistence.Extensions;

public static class RepositoryProductExtensions
{
	public static IQueryable<Product> FilterProducts(this IQueryable<Product> products, double minPrice, double maxPrice) =>
		products.Where(e => e.Price >= minPrice && e.Price <= maxPrice);

	public static IQueryable<Product> Search(this IQueryable<Product> products, string searchTerm)
	{
		if (string.IsNullOrWhiteSpace(searchTerm))
			return products;

		var lowerCaseTerm = searchTerm.Trim().ToLower();
		return products.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
	}

	public static IQueryable<Product> Sort(this IQueryable<Product> products, string orderByQueryString)
	{
		if (string.IsNullOrWhiteSpace(orderByQueryString))
			return products.OrderBy(e => e.Name);

		var orderQuery = OrderQueryBuilder.CreateOrderQuery<Product>(orderByQueryString);

		if (string.IsNullOrWhiteSpace(orderQuery))
			return products.OrderBy(e => e.Name);

		return products.OrderBy(orderQuery);
	}
}

