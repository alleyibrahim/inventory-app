using InventoryApi.Application.Common;
using InventoryApi.Application.Products.Dtos;
using InventoryApi.Repositories;
using MediatR;

namespace InventoryApi.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IProductRepository repository)
    : IRequestHandler<UpdateProductCommand, Result<ProductDto?>>
{
    public async Task<Result<ProductDto?>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id);
        if (product is null) return Result<ProductDto?>.Success(null);

        if (await repository.SkuExistsAsync(request.Sku, request.Id))
            return Result<ProductDto?>.Failure($"SKU '{request.Sku}' is already in use.");

        product.Name = request.Name;
        product.Sku = request.Sku;
        product.Price = request.Price;
        product.StockQuantity = request.StockQuantity;
        product.Category = request.Category;
        product.UpdatedAt = DateTime.UtcNow;

        var updated = await repository.UpdateAsync(product);
        return Result<ProductDto?>.Success(new ProductDto(updated.Id, updated.Name, updated.Sku, updated.Price, updated.StockQuantity, updated.Category, updated.CreatedAt, updated.UpdatedAt));
    }
}
