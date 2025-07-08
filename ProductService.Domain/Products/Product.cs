using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Products;

public class Product
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public double Price { get; set; }
	public bool Availability { get; set; }
	public DateTime CreationDate { get; set; }
	public Guid UserId { get; set; }
}
