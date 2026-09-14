using Taskivo_DTO;
using Taskivo_Infrastructure.Repositories;
using Taskivo_Queries;
using Taskivo_Queries.Task;

public class GetAllTaskQueryHandler:IQueryHandler<GetAllTaskQuery, List<TaskDto>>
{
    private readonly ITaskRepository _taskRepository;

    public GetAllTaskQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public async Task<List<TaskDto>> Handle(GetAllTaskQuery query, CancellationToken cancellationToken = default)
    {
        // Implement the logic to retrieve all tasks from the data source
        // For demonstration purposes, returning a sample list of TaskDto
         var tasks = await _taskRepository.GetAllAsync(cancellationToken);
         return [.. tasks.Select(task => new TaskDto
         {
             Id = task.Id,
             Title = task.Title,
             DueDate = task.DueDate,
             Priority = task.Priority,
             Status = task.Status,
             Description = task.Description,
             CompletedAt = task.CompletedAt,
             CreatedAt = task.CreatedAt,
             UpdatedAt = task.UpdatedAt
         })];
    }
}