namespace Taskivo_Infrastructure.Models;

public class RolePermissionEntity
{
    public Guid RoleId {get; set;}
    public Guid PermissionId {get; set;}
    public RoleEntity Role {get; set;} = null!;
    public PermissionEntity Permission {get; set;} = null!;
}