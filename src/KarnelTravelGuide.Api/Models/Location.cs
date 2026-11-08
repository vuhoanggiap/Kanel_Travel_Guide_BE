namespace KarnelTravelGuide.Api.Models;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? Province { get; set; }
    public string Country { get; set; } = "Việt Nam";
    public string? Description { get; set; }

    public ICollection<TouristSpot> TouristSpots { get; set; } = new List<TouristSpot>();
    public ICollection<ServiceProvider> ServiceProviders { get; set; } = new List<ServiceProvider>();
}
