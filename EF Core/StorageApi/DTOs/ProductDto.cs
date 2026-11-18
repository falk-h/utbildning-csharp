namespace StorageApi.DTOs;

public class ProductDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int Price { get; set; }
    public required int Count { get; set; }
    public int InventoryValue => Price * Count;
}