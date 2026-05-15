using System.ComponentModel.DataAnnotations;
using InventoryApi.Application.Common;
using InventoryApi.Application.Products.Dtos;
using MediatR;

namespace InventoryApi.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    [Required, MinLength(1)] string Name,
    [Required, MinLength(1)] string Sku,
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    decimal Price,
    [Range(0, int.MaxValue)] int StockQuantity,
    [Required] string Category) : IRequest<Result<ProductDto>>;
