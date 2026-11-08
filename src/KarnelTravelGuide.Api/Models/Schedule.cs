namespace KarnelTravelGuide.Api.Models;

public class Schedule
{
    public int Id { get; set; }
    public int TransportRouteId { get; set; }
    public TransportRoute TransportRoute { get; set; } = null!;
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public decimal Price { get; set; }
}
