using Microsoft.EntityFrameworkCore;
using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskEntity>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .AsNoTracking()
            .Include(x => x.StatusDetails)
            .Include(x => x.PriorityDetails)
            .Where(x => !x.IsDeleted && x.CreatedByUserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskEntity?> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .Include(x => x.StatusDetails)
            .Include(x => x.PriorityDetails)
            .FirstOrDefaultAsync(
                x => x.Id == id && x.CreatedByUserId == userId && !x.IsDeleted,
                cancellationToken);
    }

    public async Task<Guid> AddAsync(TaskEntity task, CancellationToken cancellationToken = default)
    {
        var createdTask = await _context.Tasks.AddAsync(task, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return createdTask.Entity.Id;
    }

    public async Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken = default)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(
                x => x.Id == id && x.CreatedByUserId == userId && !x.IsDeleted,
                cancellationToken);

        if (task == null)
            return;

        task.IsDeleted = true;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
