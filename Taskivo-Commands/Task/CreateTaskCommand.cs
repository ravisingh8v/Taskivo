namespace Taskivo_Commands.Task;

public class CreateTaskCommand
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
}
