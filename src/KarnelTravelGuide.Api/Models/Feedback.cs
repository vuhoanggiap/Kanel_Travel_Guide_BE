namespace KarnelTravelGuide.Api.Models;

public class Feedback
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string TargetType { get; set; } = "General";
    public int? TargetId { get; set; }
    public int Rating { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public int? ReviewedByUserId { get; set; }
    public User? ReviewedByUser { get; set; }
}
