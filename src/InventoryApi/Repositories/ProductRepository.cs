using InventoryApi.Data;
using InventoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Repositories;

public class ProductRepository(AppDbContext db) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await db.Products.OrderBy(p => p.Name).ToListAsync();

    public async Task<Product?> GetByIdAsync(Guid id) =>
        await db.Products.FindAsync(id);

    public async Task<bool> SkuExistsAsync(string sku, Guid? excludeId = null) =>
        await db.Products.AnyAsync(p => p.Sku == sku && p.Id != excludeId);

    public async Task<Product> CreateAsync(Product product)
    {
        db.Products.Add(product);
        await db.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        db.Products.Update(product);
        await db.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return false;

        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return true;
    }
}
