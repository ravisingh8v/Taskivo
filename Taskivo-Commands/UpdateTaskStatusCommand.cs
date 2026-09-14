namespace Taskivo_Commands.Task;

public class UpdateTaskStatusCommand
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public short StatusId { get; set; }
}
