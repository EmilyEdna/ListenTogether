﻿using ListenTogether.Business.Factories;
using ListenTogether.Business.Interfaces;
using ListenTogether.Business.Services;
using ListenTogether.Data.Interfaces;
using ListenTogether.Data.Repositories.Api;
using ListenTogether.Data.Repositories.Local;
using ListenTogether.Network;

namespace ListenTogether.Business;

public static class AppBuilderExtensions
{
    public static MauiAppBuilder UseBusiness(this MauiAppBuilder builder)
    {
        //网络数据平台
        builder.Services.AddSingleton<MusicNetPlatform>();

        //本地、远程服务工厂
        builder.Services.AddSingleton<IMusicServiceFactory, MusicServiceFactory>();
        builder.Services.AddSingleton<IMyFavoriteServiceFactory, MyFavoriteServiceFactory>();

        //本地服务
        builder.Services.AddSingleton<IEnvironmentConfigService, EnvironmentConfigService>();
        builder.Services.AddSingleton<IMusicNetworkService, MusicNetworkService>();
        builder.Services.AddSingleton<IPlaylistService, PlaylistService>();
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IApiLogService, ApiLogService>();
        builder.Services.AddSingleton<IMusicCacheService, MusicCacheService>();

        //数据服务
        builder.Services.AddSingleton<IEnvironmentConfigRepository, EnvironmentConfigLocalRepository>();
        builder.Services.AddSingleton<IMusicRepository, MusicApiRepository>();
        builder.Services.AddSingleton<IMusicRepository, MusicLocalRepository>();
        builder.Services.AddSingleton<IMyFavoriteRepository, MyFavoriteApiRepository>();
        builder.Services.AddSingleton<IMyFavoriteRepository, MyFavoriteLocalRepository>();
        builder.Services.AddSingleton<IPlaylistRepository, PlaylistLocalRepository>();
        builder.Services.AddSingleton<IUserApiRepository, UserApiRepository>();
        builder.Services.AddSingleton<ILogRepository, LogApiRepository>();
        builder.Services.AddSingleton<IMusicCacheRepository, MusicCacheRepository>();
        return builder;
    }
}
