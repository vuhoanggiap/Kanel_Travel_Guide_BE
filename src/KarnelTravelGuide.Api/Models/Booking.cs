namespace KarnelTravelGuide.Api.Models;

public class Booking
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string BookingType { get; set; } = "Tour";
    public string Status { get; set; } = "Pending";
    public decimal TotalAmount { get; set; }
    public string ContactName { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CancelledAt { get; set; }

    public ICollection<BookingDetail> Details { get; set; } = new List<BookingDetail>();
}
