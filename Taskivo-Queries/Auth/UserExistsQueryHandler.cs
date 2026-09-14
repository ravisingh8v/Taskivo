using Taskivo_Infrastructure.Repositories;

namespace Taskivo_Queries.Auth;

public class UserExistsQueryHandler : IQueryHandler<UserExistsQuery, bool>
{
    private readonly IUserRepository _userRepository;

    public UserExistsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public System.Threading.Tasks.Task<bool> Handle(
        UserExistsQuery query, CancellationToken cancellationToken = default)
    {
        return _userRepository.ExistsAsync(query.Username, cancellationToken);
    }
}
