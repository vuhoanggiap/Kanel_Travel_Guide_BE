namespace KarnelTravelGuide.Api.Models;

public class ServiceProvider
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "Hotel";
    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Phone { get; set; }
    public int? StarRating { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<ServiceItem> Items { get; set; } = new List<ServiceItem>();
}
