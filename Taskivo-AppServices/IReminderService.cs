using Taskivo_DTO.Reminder;

namespace Taskivo_AppServices;

public interface IReminderService
{
    Task<bool> CreateAsync(CreateReminderDto reminder, Guid userId, CancellationToken cancellationToken);
    Task<List<ReminderDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<ReminderDto?> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
}