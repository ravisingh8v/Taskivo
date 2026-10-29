using Microsoft.EntityFrameworkCore;

namespace Taskivo_Infrastructure.Authorization;

public class PermissionChecker: IPermissionChecker
{
    private readonly AppDbContext _dbContext;

    public PermissionChecker(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> HasPermissionAsync(Guid userId, string permissionName)
    {
        return await _dbContext.UserRoles
            .Where(ur => ur.UserId == userId)
            .Join(
                _dbContext.RolePermissions,
                ur => ur.RoleId,
                rp => rp.RoleId,
                (ur, rp) => rp)
            .Join(
                _dbContext.Permissions,
                rp => rp.PermissionId,
                p => p.Id,
                (rp, p) => p)
            .AnyAsync(p => p.Name == permissionName);
    }
}