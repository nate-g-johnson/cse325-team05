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
            },
            new JobApplication
            {
                UserId = user.Id,
                Company = "Tailspin Toys",
                JobTitle = "Backend Developer",
                Location = "Remote",
                SalaryRange = "$75k - $90k",
                DateApplied = today.AddDays(-14),
                Status = ApplicationStatus.Interviewing,
                Interviews =
                {
                    new Interview
                    {
                        ScheduledAt = today.AddDays(-4).AddHours(14),
                        Type = "Video",
                        LocationOrLink = "https://example.com/tailspin",
                        Notes = "Met with the hiring manager."
                    },
                    new Interview
                    {
                        ScheduledAt = today.AddDays(6).AddHours(9),
                        Type = "On-site",
                        LocationOrLink = "123 Main St, Provo, UT"
                    }
                },
                Reminders =
                {
                    new Reminder { DueDate = today.AddDays(4), Note = "Review C# interview questions" }
                }
            },
            new JobApplication
            {
                UserId = user.Id,
                Company = "Litware",
                JobTitle = "Full Stack Developer",
                Location = "Denver, CO",
                SalaryRange = "$80k - $95k",
                DateApplied = today.AddDays(-40),
                Status = ApplicationStatus.Offer,
                Reminders =
                {
                    new Reminder { DueDate = today.AddDays(2), Note = "Reply to the offer" }
                }
            },
            new JobApplication
            {
                UserId = user.Id,
                Company = "Proseware",
                JobTitle = "IT Support Specialist",
                Location = "Rexburg, ID",
                DateApplied = today.AddDays(-25),
                Status = ApplicationStatus.Withdrawn,
                Notes = "Schedule didn't work with classes."
            },
            new JobApplication
            {
                UserId = user.Id,
                Company = "Wide World Importers",
                JobTitle = "Junior Software Engineer",
                Location = "Remote",
                DateApplied = today.AddDays(-5),
                Status = ApplicationStatus.Applied,
                Reminders =
                {
                    new Reminder { DueDate = today.AddDays(-1), Note = "Send portfolio link", IsCompleted = true }
                }
            },
            new JobApplication
            {
                UserId = user.Id,
                Company = "Coho Winery",
                JobTitle = "Web Technician",
                Location = "Idaho Falls, ID",
                Status = ApplicationStatus.Saved
            });

        await db.SaveChangesAsync();
    }
}
