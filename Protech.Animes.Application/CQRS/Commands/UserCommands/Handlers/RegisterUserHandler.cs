using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Exceptions;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Commands.UserCommands.Handlers;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly IUserService _userService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ICryptographyService _cryptographyService;
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterUserHandler(IUserService userService, IJwtTokenService jwtTokenService, ICryptographyService cryptographyService,
        UserManager<IdentityUser> userManager)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
        _cryptographyService = cryptographyService;
        _userManager = userManager;
    }

    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Password != request.ConfirmPassword) throw new BadRequestException("Passwords do not match");

        var userExists = await _userService.GetByEmail(request.Email);
        if (userExists is not null) throw new DuplicatedEntityException("User already exists");

        var user = new User
        {
            UserName = request.Name,
            Email = request.Email,
            Password = request.Password
        };



        var result = await _userService.Register(user);
        if (!result) throw new BadRequestException("Failed to create user");

        var createdUser = await _userService.GetByEmail(request.Email);
        if (createdUser is null) throw new NotFoundException("User not found");
         
        var jwtToken = await _jwtTokenService.GenerateToken(createdUser.Email);
        var refreshToken = await _jwtTokenService.GenerateRefreshToken(createdUser.Email);
        var userDto = new UserDto
        {
            Name = createdUser.UserName,
            Email = createdUser.Email,
            Token = jwtToken,
            RefreshToken = refreshToken
        };

        return userDto;
    }
}