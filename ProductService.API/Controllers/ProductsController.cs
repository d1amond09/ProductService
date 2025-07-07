using ProductService.Application.UseCases.Products.GetProduct;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace ProductService.API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(ISender sender) : ControllerBase
{
	[HttpGet("{id:guid}", Name = "GetProduct")]
	public async Task<IActionResult> GetProduct(Guid id)
	{
		var userDetails = await sender.Send(new GetProductQuery(id));
		return Ok(userDetails);
	}
}
