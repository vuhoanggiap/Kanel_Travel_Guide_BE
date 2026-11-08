namespace KarnelTravelGuide.Api.Models;

public class Vehicle
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string VehicleType { get; set; } = "Bus";
    public string PlateNumber { get; set; } = string.Empty;
    public int SeatCount { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
