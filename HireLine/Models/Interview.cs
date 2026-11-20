using System.ComponentModel.DataAnnotations;

namespace HireLine.Models;

/// <summary>An interview scheduled for a job application.</summary>
public class Interview
{
    public int Id { get; set; }

    public int JobApplicationId { get; set; }
    public JobApplication? JobApplication { get; set; }

    [Required]
    public DateTime ScheduledAt { get; set; }

    [Required, StringLength(50)]
    public string Type { get; set; } = string.Empty;

    [StringLength(500)]
    public string? LocationOrLink { get; set; }

    [StringLength(2000)]
    public string? Notes { get; set; }
}
