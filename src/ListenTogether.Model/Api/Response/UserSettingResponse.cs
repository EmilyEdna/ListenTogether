﻿namespace ListenTogether.Model.Api.Response
{
    public class UserSettingResponse
    {
        /// <summary>
        /// 常规设置
        /// </summary>
        public GeneralSettingResponse General { get; set; } = null!;
        /// <summary>
        /// 平台设置
        /// </summary>
        public SearchSettingResponse Search { get; set; } = null!;
        /// <summary>
        /// 播放设置
        /// </summary>
        public PlaySettingResponse Play { get; set; } = null!;
    }

    public class GeneralSettingResponse
    {
        /// <summary>
        /// 是否自动检查更新
        /// </summary>
        public bool IsAutoCheckUpdate { get; set; }

        /// <summary>
        /// 关闭时最小化到托盘
        /// </summary>
        public bool IsHideWindowWhenMinimize { get; set; }
    }

    public class SearchSettingResponse
    {
        /// <summary>
        /// 启用的平台（PlatformEnum.Netease | PlatformEnum.KuGou | PlatformEnum.MiGu）
        /// </summary>
        public int EnablePlatform { get; set; }

        /// <summary>
        /// 隐藏小于1分钟的歌曲
        /// </summary>
        public bool IsHideShortMusic { get; set; }

        /// <summary>
        /// 播放失败时关闭搜索页面
        /// </summary>
        public bool IsCloseSearchPageWhenPlayFailed { get; set; }

        /// <summary>
        /// 添加到歌单时自动播放
        /// </summary>
        public bool IsPlayWhenAddToFavorite { get; set; }
    }

    public class PlaySettingResponse
    {
        /// <summary>
        /// 仅WIFI下可播放
        /// </summary>
        public bool IsWifiPlayOnly { get; set; }

        /// <summary>
        /// 播放我的歌单前清空播放列表
        /// </summary>
        public bool IsCleanPlaylistWhenPlayMyFavorite { get; set; }

        /// <summary>
        /// 歌曲无法播放时自动跳到下一首
        /// </summary>
        public bool IsAutoNextWhenFailed { get; set; }
    }
}
