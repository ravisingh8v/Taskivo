using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure.Repositories;

public interface IUserRepository
{
    Task<UserEntity?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string username, CancellationToken cancellationToken = default);
    Task<Guid> AddAsync(UserEntity user, CancellationToken cancellationToken = default);
}
