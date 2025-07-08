using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Common.RequestFeatures.ModelParameters;
using ProductService.Application.UseCases.Products.GetProduct;
using ProductService.Application.UseCases.Products.GetProducts;
using ProductService.Application.UseCases.Products.GetProductsByUser;

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

	[HttpGet("{id:guid}", Name = "GetProduct")]
	public async Task<IActionResult> GetProduct(Guid id)
	{
		var productDetails = await sender.Send(new GetProductQuery(id));
		return Ok(productDetails);
	}

}
