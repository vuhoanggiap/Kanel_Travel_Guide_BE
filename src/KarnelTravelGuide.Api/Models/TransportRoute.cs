namespace KarnelTravelGuide.Api.Models;

public class TransportRoute
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FromPlace { get; set; } = string.Empty;
    public string ToPlace { get; set; } = string.Empty;
    public decimal DistanceKm { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
