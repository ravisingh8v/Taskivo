namespace Taskivo_Infrastructure.Models;

public class PermissionEntity
{
    public Guid Id {get; set;}
    public string Name {get; set;} = null!;
    public string? Description {get; set;}
}