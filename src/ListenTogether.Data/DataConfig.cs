﻿using ListenTogether.Model;

namespace ListenTogether.Data;

public class DataConfig
{
    internal static readonly HttpClient HttpClientWithNoToken = new();
    internal static readonly HttpClient HttpClientWithToken = new(new ApiHttpMessageHandler());

    /// <summary>
    /// API 的一些配置信息
    /// </summary>
    internal static ApiSettings ApiSetting { get; set; } = null!;
    public static void SetDataBaseConnection(string path)
    {
        DatabaseProvide.SetConnection(path);
    }

    public static void SetWebApi(string apiBaseUrl, string deviceId)
    {
        ApiSetting = new ApiSettings(apiBaseUrl, deviceId);
    }
}