using Taskivo_DTO;

namespace Taskivo_AppServices;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllTasksAsync();
    Task<TaskDto?> GetTaskByIdAsync(int id);
    Task<TaskDto> CreateTaskAsync(CreateTaskDto request);
    Task<TaskDto?> UpdateTaskAsync(int id, UpdateTaskDto request);
    Task<bool> DeleteTaskAsync(int id);
}
