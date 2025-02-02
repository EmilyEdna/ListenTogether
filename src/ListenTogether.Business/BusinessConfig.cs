﻿using JiuLing.CommonLibs.ExtensionMethods;
using ListenTogether.Data;
using ListenTogether.Model;
using ListenTogether.Model.Enums;

namespace ListenTogether.Business;

public class BusinessConfig
{
    /// <summary>
    /// 程序网络版本类型
    /// </summary>
    internal static AppNetworkEnum AppNetwork = AppNetworkEnum.Standalone;

    public static void SetDataBaseConnection(string path)
    {
        if (path.IsEmpty())
        {
            throw new ArgumentException("本地数据库配置参数错误");
        }
        DataConfig.SetDataBaseConnection(path);
    }
    public static void SetWebApi(string apiBaseUrl, string deviceId)
    {
        if (apiBaseUrl.IsEmpty() || deviceId.IsEmpty())
        {
            throw new ArgumentException("Web API配置参数错误");
        }
        DataConfig.SetWebApi(apiBaseUrl, deviceId);
        AppNetwork = AppNetworkEnum.Online;
    }
}