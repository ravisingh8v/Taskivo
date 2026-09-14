using Taskivo_Commands.Task;
using Taskivo_Infrastructure.Repositories;

namespace Taskivo_Commands;

public class DeleteTaskCommandHandler : ICommandHandler<DeleteTaskCommand, bool>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<bool> Handle(DeleteTaskCommand command, CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.GetByIdAsync(command.Id, cancellationToken);

        if (task is null)
        {
            return false;
        }

        await _taskRepository.DeleteAsync(command.Id, cancellationToken);

        return true;
    }
}
