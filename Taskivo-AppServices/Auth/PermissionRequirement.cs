using Microsoft.AspNetCore.Authorization;

namespace Taskivo_Infrastructure.Authorization;

public sealed class PermissionRequirement : IAuthorizationRequirement
{
    public string PermissionName {get;}

    public PermissionRequirement(string permissionName)
    {
        PermissionName=permissionName;
    }
}