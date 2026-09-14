using Taskivo_Commands;
using Taskivo_Commands.Task;
using Taskivo_Common.Exceptions;
using Taskivo_DTO;
using Taskivo_Queries;
using Taskivo_Queries.Task;

namespace Taskivo_AppServices;

public class TaskService : ITaskService
{
    private readonly ICommandHandler<CreateTaskCommand, Guid> _createHandler;
    private readonly IQueryHandler<GetAllTaskQuery, List<TaskDto>> _getAllHandler;
    private readonly IQueryHandler<GetTaskByIdQuery, TaskDto?> _getByIdHandler;
    private readonly ICommandHandler<UpdateTaskCommand, TaskDto?> _updateHandler;
    private readonly ICommandHandler<DeleteTaskCommand, bool> _deleteHandler;

    public TaskService(
        ICommandHandler<CreateTaskCommand, Guid> createHandler,
        IQueryHandler<GetAllTaskQuery, List<TaskDto>> getAllHandler,
        IQueryHandler<GetTaskByIdQuery, TaskDto?> getByIdHandler,
        ICommandHandler<UpdateTaskCommand, TaskDto?> updateHandler,
        ICommandHandler<DeleteTaskCommand, bool> deleteHandler)
    {
        _createHandler = createHandler;
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    public Task<List<TaskDto>> GetAllTasksAsync(CancellationToken cancellationToken = default)
    {
        return _getAllHandler.Handle(new GetAllTaskQuery(), cancellationToken);
    }

    public Task<TaskDto?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            throw new BusinessException("Task id is required.");
        }

        return _getByIdHandler.Handle(new GetTaskByIdQuery { Id = id }, cancellationToken);
    }

    public async Task<Guid> CreateTaskAsync(CreateTaskDto request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new BusinessException("Task payload is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new BusinessException("Task title is required.");
        }

        var command = new CreateTaskCommand(
            request.Title,
            request.Description,
            request.PriorityId,
            request.DueDate
        );

        return await _createHandler.Handle(command, cancellationToken);
    }

    public Task<TaskDto?> UpdateTaskAsync(Guid id, UpdateTaskDto request, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            throw new BusinessException("Task id is required.");
        }

        if (request is null)
        {
            throw new BusinessException("Task payload is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new BusinessException("Task title is required.");
        }

        var command = new UpdateTaskCommand
        {
            Id = id,
            Title = request.Title,
            Description = request.Description,
            PriorityId = request.PriorityId,
            DueDate = request.DueDate
        };

        return _updateHandler.Handle(command, cancellationToken);
    }

    public Task<bool> DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            throw new BusinessException("Task id is required.");
        }

        return _deleteHandler.Handle(new DeleteTaskCommand { Id = id }, cancellationToken);
    }
}
