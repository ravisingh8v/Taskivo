using Taskivo_Commands;
using Taskivo_Common.Exceptions;
using Taskivo_DTO.Reminder;
using Taskivo_Queries;
using Taskivo_Queries.Reminder;

namespace Taskivo_AppServices;

public class ReminderService : IReminderService
{
    private readonly ICommandHandler<CreateReminderCommand, bool> _createReminderCommand;
    private readonly IQueryHandler<GetAllReminderQuery, List<ReminderDto>> _getAllReminderQuery;
    private readonly IQueryHandler<GetReminderByIdQuery, ReminderDto?> _getReminderByIdQuery;
    public ReminderService(
        ICommandHandler<CreateReminderCommand, bool> createReminderCommand,  
        IQueryHandler<GetAllReminderQuery, List<ReminderDto>> getAllReminderQuery,
        IQueryHandler<GetReminderByIdQuery, ReminderDto?> getReminderByIdQuery
        )
    {
        _createReminderCommand = createReminderCommand;
        _getAllReminderQuery = getAllReminderQuery;
        _getReminderByIdQuery = getReminderByIdQuery;
    }

    public async Task<bool> CreateAsync(CreateReminderDto reminder, Guid userId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(reminder.Title) || reminder.ReminderAt == default)
        {
            throw new BusinessException("Task title and reminder are required.");
        }

        await _createReminderCommand.Handle(
            new CreateReminderCommand { Title = reminder.Title, Description = reminder.Description, ReminderAt = reminder.ReminderAt, UserId= userId }, cancellationToken);

        return true;
    }
    public async Task<List<ReminderDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
       return await _getAllReminderQuery.Handle(new GetAllReminderQuery{UserId = userId}, cancellationToken);
    }

    public async Task<ReminderDto?> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await _getReminderByIdQuery.Handle(new GetReminderByIdQuery{Id=id, UserId = userId}, cancellationToken);
    }

}