namespace ProductService.Application.Common.DTOs;

public record ProductSummaryDto
{
	public Guid Id { get; init; }
	public string? Name { get; init; }
	public double? Price { get; init; }
	public bool? Availability { get; init; }
	public Guid UserId { get; init; }
}