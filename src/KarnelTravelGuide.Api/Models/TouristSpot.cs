namespace KarnelTravelGuide.Api.Models;

public class TouristSpot
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;
    public int CategoryTypeId { get; set; }
    public CategoryType CategoryType { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? OpenTime { get; set; }
    public decimal TicketPrice { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<TourPackage> TourPackages { get; set; } = new List<TourPackage>();
}
