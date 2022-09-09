﻿using ListenTogether.Model.Enums;

namespace ListenTogether
{
    internal class GlobalConfig
    {
        /// <summary>
        /// App名称
        /// </summary>
        private const string AppName = "ListenTogether";
        /// <summary>
        /// 本地数据库名
        /// </summary>
        public const string LocalDatabaseName = "ListenTogether.db";
        /// <summary>
        /// App Data文件夹路径
        /// </summary>
        public static readonly string AppDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppName);
        /// <summary>
        /// 歌曲缓存路径
        /// </summary>
        public static readonly string MusicCacheDirectory = Path.Combine(AppDataDirectory, "musics");

        public static byte[] AppIcon { get; set; }

        public static Version CurrentVersion { get; set; }
        public static string CurrentVersionString => CurrentVersion.ToString();

        public static string ApiDomain { get; set; }

        public static string UpdateDomain { get; set; }

        public static EnvironmentSetting MyUserSetting { get; set; }

        /// <summary>
        /// 程序网络版本类型
        /// </summary>
        internal static AppNetworkEnum AppNetwork
        {
            get
            {
                if (ApiDomain.IsEmpty())
                {
                    return AppNetworkEnum.Standalone;
                }
                return AppNetworkEnum.Online;
            }
        }
    }
}
