namespace Taskivo_Commands.Task;

public class UpdateTaskStatusCommand
{
    public Guid Id { get; set; }
    public short StatusId { get; set; }
}
