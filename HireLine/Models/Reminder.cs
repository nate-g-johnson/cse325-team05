using System.ComponentModel.DataAnnotations;

namespace HireLine.Models;

/// <summary>A follow-up reminder for a job application. Overdue when not completed and past due.</summary>
public class Reminder
{
    public int Id { get; set; }

    public int JobApplicationId { get; set; }
    public JobApplication? JobApplication { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime DueDate { get; set; }

    [Required, StringLength(300)]
    public string Note { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }
}
