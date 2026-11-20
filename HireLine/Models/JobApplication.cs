using System.ComponentModel.DataAnnotations;

namespace HireLine.Models;

/// <summary>A job the user is tracking. Always scoped to its owner via <see cref="UserId"/>.</summary>
public class JobApplication
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Company { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string JobTitle { get; set; } = string.Empty;

    [Url, StringLength(500)]
    public string? PostingUrl { get; set; }

    [StringLength(100)]
    public string? Location { get; set; }

    [StringLength(50)]
    public string? SalaryRange { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateApplied { get; set; }

    public ApplicationStatus Status { get; set; } = ApplicationStatus.Saved;

    [StringLength(2000)]
    public string? Notes { get; set; }

    /// <summary>Id of the owning Identity user. Every query must filter on this.</summary>
    [Required]
    public string UserId { get; set; } = string.Empty;

    public List<Interview> Interviews { get; set; } = new();
    public List<Reminder> Reminders { get; set; } = new();
}
