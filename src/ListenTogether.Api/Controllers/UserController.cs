﻿using Microsoft.AspNetCore.Mvc;
using ListenTogether.Api.Authorization;
using ListenTogether.Api.Interfaces;
using ListenTogether.Model.Api;
using ListenTogether.Model.Api.Request;

namespace ListenTogether.Api.Controllers;
[ApiController]
[Route("api/user")]
public class UserController : ApiBaseController
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IUserService _userService;
    public UserController(IHostEnvironment hostEnvironment, IUserService userService)
    {
        _hostEnvironment = hostEnvironment;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("reg")]
    public async Task<IActionResult> RegisterAsync([FromForm] UserRegisterRequest registerUser)
    {
        if (Request.Form.Files.Count != 1)
        {
            return Ok(new Result(101, "头像未成功上传"));
        }
        var file = Request.Form.Files[0];
        if (file.Length == 0)
        {
            return Ok(new Result(102, "文件不能为空"));
        }

        string fileName = $"{JiuLing.CommonLibs.GuidUtils.GetFormatN()}{file.FileName.Substring(file.FileName.LastIndexOf("."))}";
        var uploadDirectory = "uploads";
        var avatarDirectory = "avatars";

        var directory = Path.Combine(_hostEnvironment.ContentRootPath, uploadDirectory, avatarDirectory);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        var path = Path.Combine(directory, fileName);

        await using (var stream = System.IO.File.Create(path))
        {
            await file.CopyToAsync(stream);
        }

        string avatarUrl = $"/{uploadDirectory}/{avatarDirectory}/{fileName}";
        var response = await _userService.RegisterAsync(registerUser, avatarUrl);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("{deviceId}/login")]
    public async Task<IActionResult> LoginAsync(UserRequest user, string deviceId)
    {
        var response = await _userService.LoginAsync(user, deviceId);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("{deviceId}/refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync(AuthenticateRequest model, string deviceId)
    {
        var response = await _userService.RefreshTokenAsync(model, deviceId);
        return Ok(response);
    }

    [Authorize]
    [HttpPost("{deviceId}/logout")]
    public async Task<IActionResult> LogoutAsync(string deviceId)
    {
        await _userService.LogoutAsync(UserId, deviceId);
        return Ok("ok");
    }
}