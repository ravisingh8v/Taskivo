using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure.Authorization;

public interface IRoleProvider
{
    Task<List<string>> GetRoleNamesAsync(Guid userId);
    Task<List<RoleEntity>> GetAllRoleNamesAsync();
    Task<RoleEntity?> GetDefaultRoleAsync();
}