using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskEntity>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<TaskEntity?> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task<Guid> AddAsync(TaskEntity task, CancellationToken cancellationToken = default);
    Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
}
