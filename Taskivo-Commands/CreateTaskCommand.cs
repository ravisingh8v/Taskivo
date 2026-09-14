namespace Taskivo_Commands.Task;

public record CreateTaskCommand(
    string Title,
    string? Description,
    short PriorityId,
    DateTime? DueDate,
    Guid CreatedByUserId
);
