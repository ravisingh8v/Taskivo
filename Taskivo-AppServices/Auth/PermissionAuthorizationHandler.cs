using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Taskivo_Common.Identity;

namespace Taskivo_Infrastructure.Authorization;

public class PermissionAuthorizationHandler: AuthorizationHandler<PermissionRequirement>{
    private readonly IPermissionChecker _permissionChecker;

    public PermissionAuthorizationHandler(IPermissionChecker permissionChecker)
    {
        _permissionChecker = permissionChecker;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext authorizationContext, PermissionRequirement requirement)
    {
        var userIdClaim = authorizationContext.User.FindFirst(JwtClaimNames.UserId);

        if(userIdClaim is null)
        {
            return;
        }
        if(!Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return;
        }

        var hasPermission = await _permissionChecker.HasPermissionAsync(userId, requirement.PermissionName);

        if (hasPermission)
        {
            authorizationContext.Succeed(requirement);
        }

    }
}