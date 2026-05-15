using InventoryApi.Application.Products.Dtos;
using MediatR;

namespace InventoryApi.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;
