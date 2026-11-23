using HireLine.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HireLine.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<Interview> Interviews { get; set; }
    public DbSet<Reminder> Reminders { get; set; }
}
