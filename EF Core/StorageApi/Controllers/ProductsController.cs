using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageApi.Data;
using StorageApi.DTOs;
using StorageApi.Models;

namespace StorageApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(StorageContext context) : ControllerBase
{
    // GET: api/Products
    [HttpGet]
    public IAsyncEnumerable<Product> GetProduct() =>
        context.Product.AsNoTracking().AsAsyncEnumerable();

    // GET: api/Products/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await context.Product.AsNoTracking()
            .Where(p => p.Id == id)
            .SingleOrDefaultAsync();

        return product == null
            ? NotFound()
            : new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Count = product.Count
            };
    }

    // PUT: api/Products/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutProduct(int id, CreateProductDto productDto)
    {
        var rowsAffected = await context.Product
            .Where(p => p.Id == id)
            .ExecuteUpdateAsync(setter =>
            {
                setter.SetProperty(p => p.Name, productDto.Name);
                setter.SetProperty(p => p.Price, productDto.Price);
                setter.SetProperty(p => p.Category, productDto.Category);
                setter.SetProperty(p => p.Shelf, productDto.Shelf);
                setter.SetProperty(p => p.Count, productDto.Count);
                setter.SetProperty(p => p.Description, productDto.Description);
            });
        return rowsAffected switch
        {
            0 => NotFound(),
            1 => NoContent(),
            _ => throw new UnreachableException(),
        };
    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<ProductDto>> PostProduct(CreateProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
            Category = productDto.Category,
            Shelf = productDto.Shelf,
            Count = productDto.Count,
            Description = productDto.Description,
        };
        context.Product.Add(product);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    // DELETE: api/Products/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var rowsAffected = await context.Product.Where(p => p.Id == id).ExecuteDeleteAsync();
        return rowsAffected switch
        {
            0 => NotFound(),
            1 => NoContent(),
            _ => throw new UnreachableException(),
        };
    }

    // GET: api/Products/stats
    [HttpGet("stats")]
    public async Task<StatsDto> GetStats() {
        var results = await context.Product
            .GroupBy(_ => 1) // Hack to make EF call multiple aggregate functions in a single query
            .Select(
                group => new {
                    ProductCount = group.Count(),
                    TotalValue = group.Sum(p => p.Count * p.Price),
                    AveragePrice = group.Average(p => (decimal)p.Price),
                }
            )
            .FirstOrDefaultAsync();

        return new StatsDto {
            ProductCount = results?.ProductCount ?? 0,
            TotalValue = results?.TotalValue ?? 0,
            AveragePrice = results?.AveragePrice,
        };
    }
}