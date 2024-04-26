using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using Protech.Animes.API.Models;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.UseCases.AuthUseCases;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly RegisterUserUseCase _registerUserUseCase;
    private readonly LoginUserUseCase _loginUserUseCase;
    private readonly ILogger<AuthController> _logger;


    public AuthController(
        RegisterUserUseCase RegisterUserUseCase,
        LoginUserUseCase LoginUserUseCase,
        ILogger<AuthController> logger
        )
    {
        _registerUserUseCase = RegisterUserUseCase;
        _loginUserUseCase = LoginUserUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserDto), 201)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {
        try
        {
            _logger.LogInformation("Register user called");

            var user = await _registerUserUseCase.Execute(registerUserDto);

            _logger.LogInformation("User registered");

            return CreatedAtAction(nameof(Register), user);
        }
        catch (BadRequestException ex)
        {
            _logger.LogWarning(ex, "Bad request");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 400 };
            return BadRequest(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering the user");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Login a user.
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(UserDto), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Login(LoginUserDto loginUserDto)
    {
        try
        {
            _logger.LogInformation("Login user called");

            var userWithToken = await _loginUserUseCase.Execute(loginUserDto.Email, loginUserDto.Password);

            _logger.LogInformation("User logged in");

            return Ok(userWithToken);
        }
        catch (InvalidCredentialException ex)
        {
            _logger.LogWarning(ex, "Invalid credentials");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 400 };
            return BadRequest(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while logging in the user");

            return StatusCode(500);
        }
    }
}