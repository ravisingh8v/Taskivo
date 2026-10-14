using Taskivo_DTO.Auth;

namespace Taskivo_AppServices;

public interface IAuthService
{
    Task<AuthResponseDto> SignupAsync(SignupRequestDto request, CancellationToken cancellationToken = default);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
}
