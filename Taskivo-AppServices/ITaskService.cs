using Taskivo_DTO;

namespace Taskivo_AppServices;

public interface ITaskService
{
    Task<List<TaskDto>> GetAllTasksAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<TaskDto?> GetTaskByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task<Guid> CreateTaskAsync(CreateTaskDto request, Guid userId, CancellationToken cancellationToken = default);
    Task<TaskDto?> UpdateTaskAsync(Guid id, UpdateTaskDto request, Guid userId, CancellationToken cancellationToken = default);
    Task<TaskDto?> UpdateTaskStatusAsync(Guid id, UpdateTaskStatusDto request, Guid userId, CancellationToken cancellationToken = default);
    Task<bool> DeleteTaskAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
}
