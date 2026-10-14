using Taskivo_Infrastructure.Models;
using Taskivo_Infrastructure.Repositories;

namespace Taskivo_Commands.Auth;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserEntity>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async System.Threading.Tasks.Task<UserEntity> Handle(
        CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = new UserEntity
        {
            Id = command.Id,
            Username = command.Username,
            FirstName = command.FirstName,
            LastName = command.LastName,
            PasswordHash = command.PasswordHash,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user, cancellationToken);
        return user;
    }
}
