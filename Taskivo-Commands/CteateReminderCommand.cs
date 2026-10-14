using Taskivo_DTO.Reminder;

namespace Taskivo_Commands;

public class CreateReminderCommand: CreateReminderDto
{
     public Guid UserId { get; set; }
}