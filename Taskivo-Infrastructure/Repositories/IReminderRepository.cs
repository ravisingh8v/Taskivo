using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure.Repositories;
public interface IReminderRepository
{
Task AddAsync(ReminderEntity reminder, CancellationToken cancellationToken = default);    
Task<List<ReminderEntity>> GetAsync(Guid userId, CancellationToken cancellationToken = default);
Task<ReminderEntity?> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
//  Task<List<ReminderEntity>> GetDueAsync(DateTime currentTime, CancellationToken cancellationToken = default);

}