namespace KarnelTravelGuide.Api.Models;

public class Promotion
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Code { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal DiscountPercent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? TourPackageId { get; set; }
    public TourPackage? TourPackage { get; set; }
    public bool IsActive { get; set; } = true;
}
