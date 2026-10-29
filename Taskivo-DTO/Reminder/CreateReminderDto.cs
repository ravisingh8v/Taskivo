namespace Taskivo_DTO.Reminder;

public class CreateReminderDto
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime ReminderAt { get; set; }
}