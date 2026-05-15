using InventoryApi.Application.Products.Commands.CreateProduct;
using InventoryApi.Application.Products.Commands.DeleteProduct;
using InventoryApi.Application.Products.Commands.UpdateProduct;
using InventoryApi.Application.Products.Queries.GetAllProducts;
using InventoryApi.Application.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await mediator.Send(new GetAllProductsQuery()));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await mediator.Send(new GetProductByIdQuery(id));
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccess) return Conflict(new { message = result.Error });
        return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
    {
        var result = await mediator.Send(command with { Id = id });
        if (!result.IsSuccess) return Conflict(new { message = result.Error });
        return result.Value is null ? NotFound() : Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await mediator.Send(new DeleteProductCommand(id));
        return deleted ? NoContent() : NotFound();
    }
}
