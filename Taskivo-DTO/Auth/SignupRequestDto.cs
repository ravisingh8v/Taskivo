using System.ComponentModel.DataAnnotations;

namespace Taskivo_DTO.Auth;

public class SignupRequestDto
{
    [Required, MaxLength(50)]
    public string Username { get; set; } = null!;
    [Required, MaxLength(100)]
    public string FirstName { get; set; } = null!;
    [Required, MaxLength(100)]
    public string LastName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}
