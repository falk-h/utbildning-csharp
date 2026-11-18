using System.ComponentModel.DataAnnotations;

namespace StorageApi.DTOs;

public class CreateProductDto
{
    [StringLength(512)] public required string Name { get; set; }
    [Range(0, int.MaxValue)] public required int Price { get; set; }
    [StringLength(512)] public required string Category { get; set; }
    [StringLength(512)] public required string Shelf { get; set; }
    [Range(0, int.MaxValue)] public required int Count { get; set; }
    [StringLength(512)] public required string Description { get; set; }
}