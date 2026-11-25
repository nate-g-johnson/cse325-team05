using HireLine.Data;
using HireLine.Models;
using Microsoft.EntityFrameworkCore;

namespace HireLine.Services;

/// <summary>
/// Create, read, update and delete for job applications.
/// Every method takes the logged-in user's id so users only see their own data.
/// </summary>
public class JobApplicationService
{
    private readonly ApplicationDbContext _db;

    public JobApplicationService(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <summary>Gets all applications for this user, newest first.</summary>
    /// <param name="userId">Id of the logged-in user.</param>
    /// <returns>The user's applications.</returns>
    public async Task<List<JobApplication>> GetAllAsync(string userId)
    {
        return await _db.JobApplications
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.DateApplied)
            .ToListAsync();
    }

    /// <summary>Gets one application with its interviews and reminders.</summary>
    /// <param name="id">Id of the application.</param>
    /// <param name="userId">Id of the logged-in user.</param>
    /// <returns>The application, or null if it is not found for this user.</returns>
    public async Task<JobApplication?> GetByIdAsync(int id, string userId)
    {
        return await _db.JobApplications
            .Include(a => a.Interviews)
            .Include(a => a.Reminders)
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
    }

    /// <summary>Adds a new application owned by this user.</summary>
    /// <param name="application">The application to add.</param>
    /// <param name="userId">Id of the logged-in user.</param>
    public async Task CreateAsync(JobApplication application, string userId)
    {
        application.UserId = userId;
        _db.JobApplications.Add(application);
        await _db.SaveChangesAsync();
    }

    /// <summary>Saves changes to an existing application.</summary>
    /// <param name="application">The edited application.</param>
    /// <param name="userId">Id of the logged-in user.</param>
    /// <returns>False if the application is not found for this user.</returns>
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

    /// <summary>Deletes an application along with its interviews and reminders.</summary>
    /// <param name="id">Id of the application.</param>
    /// <param name="userId">Id of the logged-in user.</param>
    /// <returns>False if the application is not found for this user.</returns>
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
