using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TaskItem?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TaskItem> AddAsync(TaskItem task)
    {
        throw new NotImplementedException();
    }

    public Task<TaskItem?> UpdateAsync(TaskItem task)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
