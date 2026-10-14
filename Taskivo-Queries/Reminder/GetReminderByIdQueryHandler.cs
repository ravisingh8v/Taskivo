using Taskivo_DTO.Reminder;
using Taskivo_Infrastructure.Repositories;

namespace Taskivo_Queries.Reminder;
public class GetReminderByIdQueryHandler:IQueryHandler<GetReminderByIdQuery, ReminderDto?>
{
    private readonly IReminderRepository _repository;
public GetReminderByIdQueryHandler(IReminderRepository repository)
    {
        _repository = repository;
    }

    public async Task<ReminderDto?> Handle(GetReminderByIdQuery query, CancellationToken cancellationToken)
    {
        var reminder = await _repository.GetByIdAsync(query.Id, query.UserId, cancellationToken);
        if(reminder is null)
        {
            return null;
        }
        return new ReminderDto
        {
            Id= reminder.Id,
            Title= reminder.Title,
            Description= reminder.Description,
            ReminderAt = reminder.ReminderAt,
            IsSent = reminder.IsSent,
            SentAt = reminder.SentAt,
            CreatedAt = reminder.CreatedAt
        };
    }    
}