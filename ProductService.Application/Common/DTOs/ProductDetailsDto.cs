namespace ProductService.Application.Common.DTOs;

public record ProductDetailsDto
{
	public Guid Id { get; init; }
	public string? Name { get; init; }
	public string? Description { get; init; }
	public double? Price { get; init; }
	public bool? Availability { get; init; }
	public DateTime CreationDate { get; init; }
	public Guid UserId { get; init; }
}