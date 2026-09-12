using Taskivo_Commands;
using Taskivo_Commands.Task;
using Taskivo_DTO;

namespace Taskivo_AppServices;

public class TaskService : ITaskService
{
    private readonly ICommandHandler<CreateTaskCommand, Guid> _createHandler;

    public TaskService(ICommandHandler<CreateTaskCommand, Guid> createHandler)
    {
        _createHandler = createHandler;
    }

    public Task<IEnumerable<TaskDto>> GetAllTasksAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TaskDto?> GetTaskByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CreateTaskAsync(CreateTaskDto request, CancellationToken cancellationToken = default)
    {
        var command = new CreateTaskCommand(
            request.Title ?? string.Empty,
            request.Description,
            request.Priority,           
            request.DueDate
        );

        return await _createHandler.Handle(command, cancellationToken);
    }

    public Task<TaskDto?> UpdateTaskAsync(int id, UpdateTaskDto request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTaskAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
