using InventoryApi.Application.Products.Dtos;
using InventoryApi.Repositories;
using MediatR;

namespace InventoryApi.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductRepository repository)
    : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id);
        if (product is null) return null;

        return new ProductDto(product.Id, product.Name, product.Sku, product.Price, product.StockQuantity, product.Category, product.CreatedAt, product.UpdatedAt);
    }
}
