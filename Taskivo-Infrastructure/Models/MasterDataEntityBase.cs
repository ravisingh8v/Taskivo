using System.ComponentModel.DataAnnotations;

namespace Taskivo_Infrastructure.Models;

public abstract class MasterDataEntityBase
{
    [Key]
    public short Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(7)]
    public string ColorHex { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
