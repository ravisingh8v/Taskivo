namespace Taskivo_DTO.Reminder;

public class ReminderDto
{
    public Guid Id {get; set;}
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime ReminderAt { get; set; }
    public bool IsSent {get; set;}
    public DateTime? SentAt {get; set;}
    public DateTime CreatedAt{get; set;}
    
}