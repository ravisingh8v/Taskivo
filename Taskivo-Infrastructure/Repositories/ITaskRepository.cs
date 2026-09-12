using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskEntity>> GetAllAsync();
    Task<TaskEntity> GetByIdAsync(Guid id);
    void AddAsync(TaskEntity task);
    void UpdateAsync(TaskEntity task);
    void DeleteAsync(Guid id);
}
