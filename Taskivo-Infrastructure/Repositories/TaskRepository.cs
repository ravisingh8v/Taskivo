using Microsoft.EntityFrameworkCore;
using Taskivo_Common.Exceptions;
using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Tasks
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to fetch tasks from the database.", ex);
        }
    }

    public async Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(
                    x => x.Id == id && !x.IsDeleted,
                    cancellationToken);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to fetch task by id from the database.", ex);
        }
    }

    public async Task<Guid> AddAsync(TaskEntity task, CancellationToken cancellationToken = default)
    {
        try
        {
            var createdTask = await _context.Tasks.AddAsync(task, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return createdTask.Entity.Id;
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create task in the database.", ex);
        }
    }

    public async Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to update task in the database.", ex);
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(
                    x => x.Id == id && !x.IsDeleted,
                    cancellationToken);

            if (task == null)
                return;

            task.IsDeleted = true;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to delete task from the database.", ex);
        }
    }
}
