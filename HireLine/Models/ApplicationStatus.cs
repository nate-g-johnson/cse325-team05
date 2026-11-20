namespace HireLine.Models;

/// <summary>Where a job application currently stands.</summary>
public enum ApplicationStatus
{
    Saved,
    Applied,
    Interviewing,
    Offer,
    Rejected,
    Withdrawn
}
