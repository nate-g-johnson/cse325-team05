using HireLine.Data;
using HireLine.Models;
using Microsoft.EntityFrameworkCore;

namespace HireLine.Services;

// Create, read, update and delete for job applications.
// Every method takes the logged-in user's id so users only see their own data.
public class JobApplicationService
{
    private readonly ApplicationDbContext _db;

    public JobApplicationService(ApplicationDbContext db)
    {
        _db = db;
    }

    // Get all applications for this user.
    public async Task<List<JobApplication>> GetAllAsync(string userId)
    {
        return await _db.JobApplications
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.DateApplied)
            .ToListAsync();
    }

    // Get one application (with its interviews and reminders), or null if not found.
    public async Task<JobApplication?> GetByIdAsync(int id, string userId)
    {
        return await _db.JobApplications
            .Include(a => a.Interviews)
            .Include(a => a.Reminders)
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
    }

    // Add a new application for this user.
    public async Task CreateAsync(JobApplication application, string userId)
    {
        application.UserId = userId;
        _db.JobApplications.Add(application);
        await _db.SaveChangesAsync();
    }

    // Save changes to an existing application. Returns false if it isn't this user's.
    public async Task<bool> UpdateAsync(JobApplication application, string userId)
    {
        var existing = await _db.JobApplications
            .FirstOrDefaultAsync(a => a.Id == application.Id && a.UserId == userId);
        if (existing == null) return false;

        _db.Entry(existing).CurrentValues.SetValues(application);
        existing.UserId = userId;
        await _db.SaveChangesAsync();
        return true;
    }

    // Delete an application. Returns false if it isn't this user's.
    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var existing = await _db.JobApplications
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        if (existing == null) return false;

        _db.JobApplications.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
