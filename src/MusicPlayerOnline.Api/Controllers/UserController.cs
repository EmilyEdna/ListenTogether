﻿using Microsoft.AspNetCore.Mvc;
using MusicPlayerOnline.Api.Authorization;
using MusicPlayerOnline.Api.Interfaces;
using MusicPlayerOnline.Model.ApiRequest;

namespace MusicPlayerOnline.Api.Controllers;
[ApiController]
[Route("api/user")]
public class UserController : ApiBaseController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("reg")]
    public async Task<IActionResult> Register(User user)
    {
        var response = await _userService.Register(user);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("{deviceId}/login")]
    public async Task<IActionResult> Login(User user, string deviceId)
    {
        var response = await _userService.Login(user, deviceId);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("{deviceId}/refresh-token")]
    public async Task<IActionResult> RefreshToken(AuthenticateInfo model, string deviceId)
    {
        var response = await _userService.RefreshToken(model, deviceId);
        return Ok(response);
    }

    [Authorize]
    [HttpPost("{deviceId}/logout")]
    public async Task<IActionResult> Logout(string deviceId)
    {
        await _userService.Logout(UserId, deviceId);
        return Ok("ok");
    }
}