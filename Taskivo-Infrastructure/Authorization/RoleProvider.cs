using Microsoft.EntityFrameworkCore;
using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure.Authorization;

public class RoleProvider : IRoleProvider
{
    private readonly AppDbContext _dbContext;
    public RoleProvider(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<string>> GetRoleNamesAsync(Guid userId)
    {
        return await _dbContext.UserRoles.Where(x=> x.UserId == userId).Join(_dbContext.Roles, ur=> ur.RoleId, r=> r.Id, (ur, r)=> r.Name).ToListAsync();
    }
    public async Task<List<RoleEntity>> GetAllRoleNamesAsync()
    {
        return await _dbContext.Roles.ToListAsync();
    }
    public async Task<RoleEntity?> GetDefaultRoleAsync()
    {
        return await _dbContext.Roles.Where(r=> r.Name == "User").FirstOrDefaultAsync();
    }
    
}