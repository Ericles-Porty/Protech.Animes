using Microsoft.AspNetCore.Mvc;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.UseCases;

namespace Protech.Animes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly RegisterUserUseCase _registerUserUseCase;
    private readonly LoginUserUseCase _loginUserUseCase;
    private readonly ILogger<AuthController> _logger;


    public AuthController(RegisterUserUseCase RegisterUserUseCase, LoginUserUseCase LoginUserUseCase, ILogger<AuthController> logger)
    {
        _registerUserUseCase = RegisterUserUseCase;
        _loginUserUseCase = LoginUserUseCase;
        _logger = logger;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(UserDto), 201)]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {
        var user = await _registerUserUseCase.Execute(registerUserDto);

        return CreatedAtAction(nameof(Register), user);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(UserDto), 200)]
    public async Task<IActionResult> Login(LoginUserDto loginUserDto)
    {
        var userWithToken = await _loginUserUseCase.Execute(loginUserDto.Email, loginUserDto.Password);

        return Ok(userWithToken);
    }
}