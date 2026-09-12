namespace Taskivo_DTO;

public class UpdateTaskDto
{
  public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public short Priority { get; set; }
    public DateTime? DueDate { get; set; }
}   
