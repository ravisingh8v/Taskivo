namespace Taskivo_Infrastructure.Models;

public class ReminderEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public UserEntity User {get; set;} = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime ReminderAt { get; set; }
    public bool IsSent {get; set;}
    public DateTime? SentAt { get; set; }
    public DateTime CreatedAt {get; set;}
    public DateTime? UpdatedAt {get; set;}
    public DateTime? DeletedAt {get; set;}
}