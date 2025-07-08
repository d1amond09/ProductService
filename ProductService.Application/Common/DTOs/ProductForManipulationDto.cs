using System.ComponentModel.DataAnnotations;

namespace ProductService.Application.Common.DTOs;

public record ProductForManipulationDto
{
	[Required(ErrorMessage = "Product name is a required field.")]
	[MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
	public string? Name { get; init; }

	[MaxLength(500, ErrorMessage = "Maximum length for the Description is 500 characters.")]
	public string? Description { get; init; }

	[Required(ErrorMessage = "Price is a required field.")]
	[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
	public double? Price { get; init; }
}

public record ProductForUpdateDto : ProductForManipulationDto;
public record ProductForCreationDto : ProductForManipulationDto;
