namespace KarnelTravelGuide.Api.Models;

public class ServiceItem
{
    public int Id { get; set; }
    public int ServiceProviderId { get; set; }
    public ServiceProvider ServiceProvider { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string ItemType { get; set; } = "Room";
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public bool IsAvailable { get; set; } = true;
}
