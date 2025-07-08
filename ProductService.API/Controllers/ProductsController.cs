using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Common.DTOs;
using ProductService.Application.Common.Interfaces;
using ProductService.Application.Common.RequestFeatures.ModelParameters;
using ProductService.Application.UseCases.Products.AddProduct;
using ProductService.Application.UseCases.Products.GetProduct;
using ProductService.Application.UseCases.Products.GetProducts;
using ProductService.Application.UseCases.Products.GetProductsByUser;
using ProductService.Application.UseCases.Products.RemoveProduct;
using ProductService.Application.UseCases.Products.UpdateProduct;

namespace ProductService.API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(ISender sender) : ControllerBase
{
	[HttpGet(Name = "GetProducts")]
	public async Task<IActionResult> GetProducts([FromQuery] ProductParameters productParams)
	{
		var pagedProducts = await sender.Send(new GetProductsQuery(productParams, false));

		Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedProducts.MetaData));

		return Ok(pagedProducts.Items);
	}

	[HttpGet("user/{id:guid}", Name = "GetProductsByUser")]
	public async Task<IActionResult> GetProductsByUser(Guid id, [FromQuery] ProductParameters productParams)
	{
		var pagedProducts = await sender.Send(new GetProductsByUserQuery(id, productParams, false));

		Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedProducts.MetaData));

		return Ok(pagedProducts.Items);
	}

	[HttpGet("{id:guid}", Name = "GetProductById")]
	public async Task<IActionResult> GetProductById(Guid id)
	{
		var productDetails = await sender.Send(new GetProductQuery(id));
		return Ok(productDetails);
	}

	[HttpPost]
	[Authorize]
	[ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
	public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationDto request)
	{
		var newProductId = await sender.Send(new AddProductCommand(request));

		return CreatedAtAction(
			nameof(GetProductById),
			new { id = newProductId },
			new { id = newProductId }
		);
	}

	[HttpDelete("{id:guid}")]
	[Authorize]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	public async Task<IActionResult> DeleteProduct(Guid id)
	{
		await sender.Send(new RemoveProductCommand(id));
		return NoContent();
	}

	[HttpPut("{id:guid}")]
	[Authorize] 
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductForUpdateDto request)
	{
		await sender.Send(new UpdateProductCommand(id, request));
		return NoContent();
	}
}
