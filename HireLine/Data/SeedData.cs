using HireLine.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HireLine.Data;

// Adds a demo user and some sample applications so the app has data to show.
// Only called in Development (see Program.cs).
public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services, IConfiguration config)
    {
        var email = config["SeedUser:Email"];
        var password = config["SeedUser:Password"];
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return;

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var db = services.GetRequiredService<ApplicationDbContext>();

        // Create the demo user if it doesn't exist yet.
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new ApplicationUser { UserName = email, Email = email, EmailConfirmed = true };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded) return;
        }

        // Don't add sample data twice.
        if (await db.JobApplications.AnyAsync(a => a.UserId == user.Id)) return;

        var today = DateTime.Today;

        db.JobApplications.AddRange(
            new JobApplication
            {
                UserId = user.Id,
                Company = "Contoso",
                JobTitle = "Junior .NET Developer",
                Location = "Remote",
                SalaryRange = "$60k - $75k",
                DateApplied = today.AddDays(-10),
                Status = ApplicationStatus.Interviewing,
                Notes = "Referred by a classmate.",
                Interviews =
                {
                    new Interview
                    {
                        ScheduledAt = today.AddDays(3).AddHours(10),
                        Type = "Phone screen",
                        LocationOrLink = "https://example.com/meet"
                    }
                },
                Reminders =
                {
                    new Reminder { DueDate = today.AddDays(5), Note = "Send thank-you email" }
                }
            },
            new JobApplication
            {
                UserId = user.Id,
                Company = "Fabrikam",
                JobTitle = "Software Engineer Intern",
                Location = "Boise, ID",
                DateApplied = today.AddDays(-21),
                Status = ApplicationStatus.Applied,
                Reminders =
                {
                    // In the past and not completed, so it shows as overdue.
                    new Reminder { DueDate = today.AddDays(-2), Note = "Follow up with the recruiter" }
                }
            },
            new JobApplication
            {
                UserId = user.Id,
                Company = "Adventure Works",
                JobTitle = "Web Developer",
                Location = "Salt Lake City, UT",
                SalaryRange = "$70k - $85k",
                Status = ApplicationStatus.Saved,
                Notes = "Application deadline is next week."
            },
            new JobApplication
            {
                UserId = user.Id,
                Company = "Northwind",
                JobTitle = "QA Analyst",
                DateApplied = today.AddDays(-30),
                Status = ApplicationStatus.Rejected
            });

        await db.SaveChangesAsync();
    }
}
