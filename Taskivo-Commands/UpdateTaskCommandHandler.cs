using Taskivo_Commands.Task;
using Taskivo_DTO;
using Taskivo_Infrastructure.Repositories;

namespace Taskivo_Commands;

public class UpdateTaskCommandHandler : ICommandHandler<UpdateTaskCommand, TaskDto?>
{
    private readonly ITaskRepository _taskRepository;

    public UpdateTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskDto?> Handle(UpdateTaskCommand command, CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.GetByIdAsync(command.Id, cancellationToken);

        if (task is null)
        {
            return null;
        }

        task.Title = command.Title;
        task.Description = command.Description;
        task.Priority = command.Priority;
        task.DueDate = command.DueDate;
        task.UpdatedAt = DateTime.UtcNow;

        await _taskRepository.UpdateAsync(task, cancellationToken);

        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            DueDate = task.DueDate,
            CompletedAt = task.CompletedAt,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        };
    }
}
