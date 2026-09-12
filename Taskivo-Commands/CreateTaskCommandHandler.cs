using Taskivo_Commands.Task;
using Taskivo_Infrastructure.Models;
using Taskivo_Infrastructure.Repositories;

namespace Taskivo_Commands;

public class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand, Guid>
{
    public readonly ITaskRepository _taskRepository;

    public CreateTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async System.Threading.Tasks.Task<Guid> Handle(
        CreateTaskCommand command,
        CancellationToken cancellationToken = default)
    {
        var task = new TaskEntity
        {
            Title = command.Title,
            Description = command.Description,
            Priority = command.Priority,
            DueDate = command.DueDate,
            Status = TaskStatusIds.New,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        return await _taskRepository.AddAsync(task, cancellationToken);
    }
}