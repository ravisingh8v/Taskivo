using Taskivo_DTO;

namespace Taskivo_AppServices;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllTasksAsync(CancellationToken cancellationToken = default);
    Task<TaskDto?> GetTaskByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Guid> CreateTaskAsync(CreateTaskDto request, CancellationToken cancellationToken = default);
    Task<TaskDto?> UpdateTaskAsync(int id, UpdateTaskDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteTaskAsync(int id, CancellationToken cancellationToken = default);
}
