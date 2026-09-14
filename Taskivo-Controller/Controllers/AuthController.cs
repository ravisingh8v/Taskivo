using Microsoft.AspNetCore.Mvc;
using Taskivo_AppServices;
using Taskivo_Common.Responses;
using Taskivo_DTO.Auth;

namespace Taskivo_Controller.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signup")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Signup([FromBody] SignupRequestDto request, CancellationToken cancellationToken = default)
    {
        var authResult = await _authService.SignupAsync(request, cancellationToken);

        return Ok(ApiResponse.Success(authResult, "User registered successfully."));
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        var authResult = await _authService.LoginAsync(request, cancellationToken);

        return Ok(ApiResponse.Success(authResult, "Login successful."));
    }
}
