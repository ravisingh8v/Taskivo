using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Taskivo_Common.Exceptions;
using Taskivo_DTO.Auth;
using Taskivo_Infrastructure.Models;
using Taskivo_Commands;
using Taskivo_Commands.Auth;
using Taskivo_Queries;
using Taskivo_Queries.Auth;
using Taskivo_Infrastructure.Authorization;
using Taskivo_Common.Identity;

namespace Taskivo_AppServices;

public class AuthService : IAuthService
{
    private readonly ICommandHandler<CreateUserCommand, UserEntity> _createUserHandler;
    private readonly IQueryHandler<UserExistsQuery, bool> _userExistsHandler;
    private readonly IQueryHandler<GetUserByUsernameQuery, UserEntity?> _getUserByUsernameHandler;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<UserEntity> _passwordHasher = new();

    private readonly IRoleProvider _roleProvider;

    public AuthService(
        ICommandHandler<CreateUserCommand, UserEntity> createUserHandler,
        IQueryHandler<UserExistsQuery, bool> userExistsHandler,
        IQueryHandler<GetUserByUsernameQuery, UserEntity?> getUserByUsernameHandler,
        IConfiguration configuration,
        IRoleProvider roleProvider
        )
    {
        _createUserHandler = createUserHandler;
        _userExistsHandler = userExistsHandler;
        _getUserByUsernameHandler = getUserByUsernameHandler;
        _configuration = configuration;
        _roleProvider = roleProvider;
    }

    public async Task<AuthResponseDto> SignupAsync(SignupRequestDto request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new BusinessException("Registration payload is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Username)
            || string.IsNullOrWhiteSpace(request.FirstName)
            || string.IsNullOrWhiteSpace(request.LastName)
            || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new BusinessException("Username, first name, last name, and password are required.");
        }

        var username = request.Username.Trim();

        if (await _userExistsHandler.Handle(new UserExistsQuery(username), cancellationToken))
        {
            throw new BusinessException("Username already exists.", 409);
        }



        var defaultRole = await _roleProvider.GetDefaultRoleAsync() ?? throw new InvalidOperationException("Default User role is not configured.");
        
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Username = username,
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            PasswordHash = string.Empty,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
        user = await _createUserHandler.Handle(
            new CreateUserCommand(user.Id, user.Username, user.FirstName, user.LastName, user.PasswordHash, defaultRole.Id),
            cancellationToken);



        var userRoleNames = await _roleProvider.GetRoleNamesAsync(user.Id);


        return BuildAuthResponse(user, userRoleNames);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new BusinessException("Login payload is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new BusinessException("Username and password are required.");
        }

        var user = await _getUserByUsernameHandler.Handle(new GetUserByUsernameQuery(request.Username.Trim()), cancellationToken);

        if (user is null)
        {
            throw new BusinessException("Invalid username or password.", 401);
        }

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            throw new BusinessException("Invalid username or password.", 401);
        }

        var roleNames = await _roleProvider.GetRoleNamesAsync(user.Id);

        return BuildAuthResponse(user, roleNames);
    }

    private AuthResponseDto BuildAuthResponse(UserEntity user, IEnumerable<string> roleNames)
    {
        var token = GenerateJwtToken(user, roleNames);

        return new AuthResponseDto
        {
            Token = token,
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            }
        };
    }

    private string GenerateJwtToken(UserEntity user, IEnumerable<string> roleNames)
    {
        var jwtKey = _configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new BusinessException("JWT key is not configured.", 500);
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new(JwtClaimNames.Subject, user.Id.ToString()),
            new(JwtClaimNames.UserId, user.Id.ToString()),
            new(JwtClaimNames.Username, user.Username),
            new(JwtClaimNames.Firstname, user.FirstName),
            new(JwtClaimNames.Lastname, user.LastName)
        };

        claims.AddRange(roleNames.Select(role=> new Claim(JwtClaimNames.Roles, role)));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
