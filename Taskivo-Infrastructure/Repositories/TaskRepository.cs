using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<TaskEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TaskEntity> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public void AddAsync(TaskEntity task)
    {
        throw new NotImplementedException();
    }

    public void UpdateAsync(TaskEntity task)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
