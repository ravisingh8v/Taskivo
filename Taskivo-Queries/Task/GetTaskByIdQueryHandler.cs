using Taskivo_DTO;
using Taskivo_Infrastructure.Repositories;
using Taskivo_Queries;
using Taskivo_Queries.Task;

public class GetTaskByIdQueryHandler : IQueryHandler<GetTaskByIdQuery, TaskDto?>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskDto?> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.GetByIdAsync(query.Id, cancellationToken);

        if (task is null)
        {
            return null;
        }

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
