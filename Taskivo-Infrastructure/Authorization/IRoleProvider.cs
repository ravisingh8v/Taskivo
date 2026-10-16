namespace Taskivo_Infrastructure.Authorization;

public interface IRoleProvider
{
    Task<List<string>> GetRoleNamesAsync(Guid userId);
}