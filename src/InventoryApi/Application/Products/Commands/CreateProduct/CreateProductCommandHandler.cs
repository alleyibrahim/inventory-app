using InventoryApi.Application.Common;
using InventoryApi.Application.Products.Dtos;
using InventoryApi.Models;
using InventoryApi.Repositories;
using MediatR;

namespace InventoryApi.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository repository)
    : IRequestHandler<CreateProductCommand, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (await repository.SkuExistsAsync(request.Sku))
            return Result<ProductDto>.Failure($"SKU '{request.Sku}' is already in use.");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Sku = request.Sku,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            Category = request.Category,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await repository.CreateAsync(product);
        return Result<ProductDto>.Success(new ProductDto(created.Id, created.Name, created.Sku, created.Price, created.StockQuantity, created.Category, created.CreatedAt, created.UpdatedAt));
    }
}
