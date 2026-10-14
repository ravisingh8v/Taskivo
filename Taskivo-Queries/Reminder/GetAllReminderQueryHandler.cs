using Taskivo_DTO.Reminder;
using Taskivo_Infrastructure.Repositories;

namespace Taskivo_Queries.Reminder;

public class GetAllReminderQueryHandler : IQueryHandler<GetAllReminderQuery, List<ReminderDto>>
{
    private readonly IReminderRepository _repository;

    public GetAllReminderQueryHandler(IReminderRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<ReminderDto>> Handle(GetAllReminderQuery query, CancellationToken cancellationToken)
    {
        var reminders = await _repository.GetAsync(query.UserId, cancellationToken);
        return reminders.Select(r => new ReminderDto { Title = r.Title, Description = r.Description, Id = r.Id, ReminderAt = r.ReminderAt, IsSent = r.IsSent, SentAt = r.SentAt, CreatedAt = r.CreatedAt }).ToList();
    }
}