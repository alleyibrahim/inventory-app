namespace InventoryApi.Application.Products.Dtos;

public record ProductDto(
    Guid Id,
    string Name,
    string Sku,
    decimal Price,
    int StockQuantity,
    string Category,
    DateTime CreatedAt,
    DateTime UpdatedAt);
