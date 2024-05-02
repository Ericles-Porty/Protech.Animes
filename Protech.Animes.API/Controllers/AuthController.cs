using System.Security.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Protech.Animes.API.Models;
using Protech.Animes.Application.CQRS.Commands.UserCommands;
using Protech.Animes.Application.CQRS.Queries.UserQueries;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Exceptions;
using Protech.Animes.Domain.Policies;

namespace Protech.Animes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMediator _mediator;


    public AuthController(
        ILogger<AuthController> logger,
        IMediator mediator
        )
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserDto), 201)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerUserCommand)
    {
        try
        {
            _logger.LogInformation("Register user called");

            var user = await _mediator.Send(registerUserCommand);

            _logger.LogInformation("User registered");

            return CreatedAtAction(nameof(Register), user);
        }
        catch (BadRequestException ex)
        {
            _logger.LogWarning(ex, "Bad request");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 400 };
            return BadRequest(error);
        }
        catch (DuplicatedEntityException)
        {
            _logger.LogWarning("Duplicated entity");

            var error = new ErrorModel { Message = "User already exists", StatusCode = 400 };
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
    [EnableRateLimiting(RateLimitPolicies.LoginAttempts)]
    [HttpPost("login")]
    [ProducesResponseType(typeof(UserDto), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery loginUserQuery)
    {
        try
        {
            _logger.LogInformation("Login user called");

            var user = await _mediator.Send(loginUserQuery);

            _logger.LogInformation("User logged in");

            return Ok(user);
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