﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ListenTogether.Api.Authorization;
using ListenTogether.Api.DbContext;
using ListenTogether.Api.Entities;
using ListenTogether.Api.ErrorHandler;
using ListenTogether.Api.Interfaces;
using ListenTogether.Api.Models;
using ListenTogether.Model.Api;
using ListenTogether.Model.Api.Request;
using ListenTogether.Model.Api.Response;

namespace ListenTogether.Api.Services;
internal class UserService : IUserService
{
    private readonly DataContext _context;
    private readonly IJwtUtils _jwtUtils;
    private readonly AppSettings _appSettings;
    public UserService(DataContext context, IJwtUtils jwtUtils, IOptions<AppSettings> appSettings)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _appSettings = appSettings.Value;
    }

    public async Task<Result> RegisterAsync(UserRegisterRequest registerUser, string avatarUrl)
    {
        var isUserExist = await _context.Users.AnyAsync(x => x.Username == registerUser.Username);
        if (isUserExist)
        {
            return new Result(1, "用户名已存在");
        }

        string salt = JiuLing.CommonLibs.GuidUtils.GetFormatN();
        string password = JiuLing.CommonLibs.Security.MD5Utils.GetStringValueToLower($"{registerUser.Password}{salt}");

        var userEntity = new UserEntity()
        {
            Username = registerUser.Username,
            Password = password,
            Salt = salt,
            IsEnable = false,
            Nickname = registerUser.Nickname,
            Avatar = avatarUrl,
            CreateTime = DateTime.Now,
        };

        _context.Users.Add(userEntity);
        var count = await _context.SaveChangesAsync();
        if (count == 0)
        {
            return new Result(2, "注册失败");
        }
        return new Result(0, "注册成功，请等待管理员审核");
    }

    public async Task<Result<UserResponse>> LoginAsync(UserRequest user, string deviceId)
    {
        var userEntity = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == user.Username.ToLower() && x.IsEnable);
        if (userEntity == null)
        {
            return new Result<UserResponse>(1, "用户名或密码不正确", null);
        }

        string password = JiuLing.CommonLibs.Security.MD5Utils.GetStringValueToLower($"{user.Password}{userEntity.Salt}");
        if (password != userEntity.Password)
        {
            return new Result<UserResponse>(1, "用户名或密码不正确", null);
        }

        var token = _jwtUtils.GenerateToken(userEntity, deviceId);
        var refreshToken = _jwtUtils.GenerateRefreshToken(deviceId);

        userEntity.RefreshTokens.RemoveAll(x => x.DeviceId == deviceId);
        userEntity.RefreshTokens.Add(refreshToken);

        _context.Update(userEntity);
        await _context.SaveChangesAsync();

        return new Result<UserResponse>(0, "", new UserResponse()
        {
            Username = userEntity.Username,
            Nickname = userEntity.Nickname,
            Avatar = userEntity.Avatar,
            Token = token,
            RefreshToken = refreshToken.Token
        });
    }

    public async Task<Result<UserResponse>> RefreshTokenAsync(AuthenticateRequest authenticateInfo, string deviceId)
    {
        var userEntity = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(r => r.DeviceId == deviceId & r.Token == authenticateInfo.RefreshToken));
        if (userEntity == null)
        {
            throw new AppException("无效Token");
        }

        var refreshToken = userEntity.RefreshTokens.Single(x => x.DeviceId == deviceId);
        if (refreshToken.IsExpired)
        {
            throw new AppException("Token已过期");
        }

        var newToken = _jwtUtils.GenerateToken(userEntity, deviceId);
        var newRefreshToken = _jwtUtils.GenerateRefreshToken(deviceId);

        userEntity.RefreshTokens.RemoveAll(x => x.DeviceId == deviceId);
        userEntity.RefreshTokens.Add(newRefreshToken);

        _context.Update(userEntity);
        await _context.SaveChangesAsync();

        return new Result<UserResponse>(0, "更新成功", new UserResponse()
        {
            Username = userEntity.Username,
            Nickname = userEntity.Nickname,
            Avatar = userEntity.Avatar,
            Token = newToken,
            RefreshToken = newRefreshToken.Token
        });
    }

    public async Task LogoutAsync(int userId, string deviceId)
    {
        var userEntity = _context.Users.SingleOrDefault(x => x.Id == userId);
        if (userEntity == null)
        {
            throw new AppException("用户不存在");
        }

        userEntity.RefreshTokens.RemoveAll(x => x.DeviceId == deviceId);
        await _context.SaveChangesAsync();
    }

    public async Task<Result<UserResponse>> GetUserInfoAsync(int id)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id && x.IsEnable);
        if (user == null)
        {
            return new Result<UserResponse>(1, "用户不存在", null);
        }

        return new Result<UserResponse>(0, "", new UserResponse()
        {
            Username = user.Username,
            Nickname = user.Nickname,
            Avatar = user.Avatar
        });
    }

    public async Task<UserEntity?> GetOneEnableAsync(int id)
    {
        var userEntity = await _context.Users.SingleOrDefaultAsync(x => x.Id == id && x.IsEnable);
        if (userEntity == null)
        {
            return null;
        }

        return userEntity;
    }
}