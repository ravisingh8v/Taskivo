namespace Taskivo_Commands.Auth;

public record CreateUserCommand(
    Guid Id,
    string Username,
    string FirstName,
    string LastName,
    string PasswordHash);
