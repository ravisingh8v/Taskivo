using Taskivo_DTO;

namespace Taskivo_AppServices;

public interface ITaskService
{
    Task<List<TaskDto>> GetAllTasksAsync(CancellationToken cancellationToken = default);
    Task<TaskDto?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> CreateTaskAsync(CreateTaskDto request, CancellationToken cancellationToken = default);
    Task<TaskDto?> UpdateTaskAsync(Guid id, UpdateTaskDto request, CancellationToken cancellationToken = default);
    Task<TaskDto?> UpdateTaskStatusAsync(Guid id, UpdateTaskStatusDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default);
}
