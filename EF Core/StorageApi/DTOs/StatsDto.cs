namespace StorageApi.DTOs;

public class StatsDto {
    public required int ProductCount { get; set; }
    public required int TotalValue { get; set; }
    public required decimal? AveragePrice { get; set; }
}