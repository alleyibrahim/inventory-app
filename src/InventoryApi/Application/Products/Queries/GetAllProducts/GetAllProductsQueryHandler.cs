using InventoryApi.Application.Products.Dtos;
using InventoryApi.Repositories;
using MediatR;

namespace InventoryApi.Application.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(IProductRepository repository)
    : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await repository.GetAllAsync();
        return products.Select(p => new ProductDto(p.Id, p.Name, p.Sku, p.Price, p.StockQuantity, p.Category, p.CreatedAt, p.UpdatedAt));
    }
}
