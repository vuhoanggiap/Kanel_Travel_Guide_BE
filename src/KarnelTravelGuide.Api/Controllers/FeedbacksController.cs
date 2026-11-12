using KarnelTravelGuide.Api.Contracts;
using KarnelTravelGuide.Api.Data;
using KarnelTravelGuide.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Api.Controllers;

[ApiController]
[Route("api/feedbacks")]
public class FeedbacksController : ControllerBase
{
    private readonly AppDbContext _db;

    public FeedbacksController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create(FeedbackRequest request)
    {
        var feedback = new Feedback
        {
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim(),
            Rating = request.Rating,
            Title = string.IsNullOrWhiteSpace(request.Title) ? null : request.Title.Trim(),
            Content = request.Content.Trim(),
            IsApproved = false,
            CreatedAt = DateTime.UtcNow
        };

        _db.Feedbacks.Add(feedback);
        await _db.SaveChangesAsync();

        return Created($"/api/feedbacks/{feedback.FeedbackId}", new
        {
            feedback.FeedbackId,
            feedback.IsApproved
        });
    }

    [HttpGet("approved")]
    public async Task<IActionResult> Approved()
    {
        var items = await _db.Feedbacks
            .AsNoTracking()
            .Where(item => item.IsApproved)
            .OrderByDescending(item => item.CreatedAt)
            .Select(item => new
            {
                item.FeedbackId,
                item.FullName,
                item.Rating,
                item.Title,
                item.Content,
                item.CreatedAt
            })
            .ToListAsync();

        return Ok(items);
    }
}
