namespace Taskivo_Infrastructure.Authorization;

public interface IPermissionChecker
{
    Task<bool>  HasPermissionAsync(Guid userId, string PermissionName);
}