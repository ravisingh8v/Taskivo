namespace Taskivo_DTO;

public class CreateTaskDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public short PriorityId { get; set; } = 1;
    public DateTime? DueDate { get; set; }
}
