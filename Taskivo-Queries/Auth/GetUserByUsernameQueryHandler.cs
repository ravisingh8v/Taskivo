using Taskivo_Infrastructure.Models;
using Taskivo_Infrastructure.Repositories;

namespace Taskivo_Queries.Auth;

public class GetUserByUsernameQueryHandler : IQueryHandler<GetUserByUsernameQuery, UserEntity?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByUsernameQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public System.Threading.Tasks.Task<UserEntity?> Handle(
        GetUserByUsernameQuery query, CancellationToken cancellationToken = default)
    {
        return _userRepository.GetByUsernameAsync(query.Username, cancellationToken);
    }
}
