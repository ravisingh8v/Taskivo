using Taskivo_DTO;
using Taskivo_Infrastructure.Repositories;
using Taskivo_Queries;
using Taskivo_Queries.Task;

public class GetAllTaskQueryHandler : IQueryHandler<GetAllTaskQuery, List<TaskDto>>
{
    private readonly ITaskRepository _taskRepository;

    public GetAllTaskQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<List<TaskDto>> Handle(GetAllTaskQuery query, CancellationToken cancellationToken = default)
    {
        var tasks = await _taskRepository.GetAllAsync(cancellationToken);

        return [.. tasks.Select(task => new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            CompletedAt = task.CompletedAt,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt,
            Status = new TaskStatusDto
            {
                Id = task.Status,
                Name = task.StatusDetails?.Name ?? "Unknown",
                ColorHex = task.StatusDetails?.ColorHex ?? "#64748B"
            },
            Priority = new TaskPriorityDto
            {
                Id = task.Priority,
                Name = task.PriorityDetails?.Name ?? "Unknown",
                ColorHex = task.PriorityDetails?.ColorHex ?? "#64748B"
            }
        })];
    }
}