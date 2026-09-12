using Taskivo_DTO;

namespace Taskivo_AppServices;

public class TaskService : ITaskService
{
    public Task<IEnumerable<TaskDto>> GetAllTasksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TaskDto?> GetTaskByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TaskDto> CreateTaskAsync(CreateTaskDto request)
    {
        throw new NotImplementedException();
    }

    public Task<TaskDto?> UpdateTaskAsync(int id, UpdateTaskDto request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTaskAsync(int id)
    {
        throw new NotImplementedException();
    }
}
