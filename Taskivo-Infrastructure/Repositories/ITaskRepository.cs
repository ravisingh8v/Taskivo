using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> AddAsync(TaskEntity task, CancellationToken cancellationToken = default);
    Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
