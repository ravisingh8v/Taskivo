namespace Taskivo_Infrastructure.Repositories;

public interface IUserRoleRepository
{
    Task AddAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
}