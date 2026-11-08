namespace KarnelTravelGuide.Api.Models;

public class CategoryType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<TouristSpot> TouristSpots { get; set; } = new List<TouristSpot>();
}
