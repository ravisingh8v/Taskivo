namespace Taskivo_Commands.Task;

public class UpdateTaskCommand
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public short PriorityId { get; set; }
    public DateTime? DueDate { get; set; }
}
