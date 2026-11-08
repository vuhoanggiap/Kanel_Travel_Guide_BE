namespace KarnelTravelGuide.Api.Models;

public class TourPackage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? TouristSpotId { get; set; }
    public TouristSpot? TouristSpot { get; set; }
    public string Description { get; set; } = string.Empty;
    public int DurationDays { get; set; }
    public decimal Price { get; set; }
    public int MaxGuests { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}
