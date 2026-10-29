using Taskivo_Infrastructure.Authorization;
using Taskivo_Infrastructure.Models;
using Taskivo_Infrastructure.Repositories;

namespace Taskivo_Commands.Auth;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserEntity>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public CreateUserCommandHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
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

        await _userRoleRepository.AddAsync(user.Id, command.RoleId, cancellationToken);
        return user;
    }
}
