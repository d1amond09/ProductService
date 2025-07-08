namespace ProductService.Domain.Products;

public class Product
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public double Price { get; set; }
	public bool Availability { get; set; } = true;
	public DateTime CreationDate { get; set; } = DateTime.UtcNow;
	public Guid UserId { get; set; }
}
