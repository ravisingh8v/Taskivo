namespace Taskivo_Infrastructure.Models;

public class RoleEntity
{
    public Guid Id {get; set;}
    public string Name {get; set;} = null!;
    public string? Description {get; set;}
    
}