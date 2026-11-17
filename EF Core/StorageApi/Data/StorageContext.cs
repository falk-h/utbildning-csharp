using Microsoft.EntityFrameworkCore;
using StorageApi.Models;

namespace StorageApi.Data;

public class StorageContext(DbContextOptions<StorageContext> options) : DbContext(options)
{
    public DbSet<Product> Product { get; set; } = null!;
}