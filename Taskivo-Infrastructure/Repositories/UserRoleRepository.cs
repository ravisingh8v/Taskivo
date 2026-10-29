using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly AppDbContext _dbContext;

    public UserRoleRepository(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    public async Task AddAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
       await _dbContext.UserRoles.AddAsync(new UserRoleEntity{UserId= userId, RoleId = roleId}, cancellationToken);
       await _dbContext.SaveChangesAsync();
    }
}