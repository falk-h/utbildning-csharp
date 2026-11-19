using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StorageApi.Models;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
public class Product {
    public int Id { get; set; }
    [StringLength(512)] public required string Name { get; set; }
    [Range(0, int.MaxValue)] public required int Price { get; set; }
    [StringLength(512)] public required string Category { get; set; }
    [StringLength(512)] public required string Shelf { get; set; }
    [Range(0, int.MaxValue)] public required int Count { get; set; }
    [StringLength(512)] public required string Description { get; set; }
}

public class ProductConfiguration: IEntityTypeConfiguration<Product> {
    public void Configure(EntityTypeBuilder<Product> builder) {
        builder.ToTable(
            b => {
                b.HasCheckConstraint("CK_Count_Nonnegative", "Count >= 0");
                b.HasCheckConstraint("CK_Price_Nonnegative", "Price >= 0");
            }
        );
    }
}