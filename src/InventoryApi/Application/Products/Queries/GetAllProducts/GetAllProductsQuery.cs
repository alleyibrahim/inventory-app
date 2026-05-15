using InventoryApi.Application.Products.Dtos;
using MediatR;

namespace InventoryApi.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;
