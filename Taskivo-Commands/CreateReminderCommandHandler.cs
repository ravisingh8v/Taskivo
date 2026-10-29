using System.Reflection.Metadata;
using Taskivo_Infrastructure.Models;
using Taskivo_Infrastructure.Repositories;

namespace Taskivo_Commands;

public class CreateReminderCommandHandler : ICommandHandler<CreateReminderCommand, bool>
{
    public readonly IReminderRepository _reminderRepository;

public CreateReminderCommandHandler(IReminderRepository reminderRepository)
    {
        _reminderRepository = reminderRepository;
    }

    public async System.Threading.Tasks.Task<bool> Handle(CreateReminderCommand command, CancellationToken cancellationToken)
    {
        var reminder = new ReminderEntity
        {
            UserId = command.UserId,
            Title = command.Title,
            Description =command.Description,
            ReminderAt = command.ReminderAt,
            IsSent = false,
            CreatedAt = DateTime.UtcNow
        };

        await _reminderRepository.AddAsync(reminder,  cancellationToken);
        return true;
    }
}