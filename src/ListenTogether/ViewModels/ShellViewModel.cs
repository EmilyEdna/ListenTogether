﻿using ListenTogether.Models;
namespace ListenTogether.ViewModels;
internal class ShellViewModel : ViewModelBase
{
    private string _title;
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    private string _icon;
    public string Icon
    {
        get => _icon;
        set
        {
            _icon = value;
            OnPropertyChanged();
        }
    }

    public string AppNetworkString => GlobalConfig.AppNetwork.GetDescription();
    public AppSection SearchResult { get; set; }
    public AppSection Playlist { get; set; }
    public AppSection MyFavorite { get; set; }
    public AppSection Playing { get; set; }
    public AppSection Settings { get; set; }

    public ShellViewModel()
    {
        SearchResult = new AppSection() { Title = "搜索", Icon = "search.png", IconDark = "search_dark.png", TargetType = typeof(SearchResultPage) };
        Playlist = new AppSection() { Title = "播放列表", Icon = "playlist.png", IconDark = "playlist_dark.png", TargetType = typeof(PlaylistPage) };
        MyFavorite = new AppSection() { Title = "我的歌单", Icon = "heart.png", IconDark = "heart_dark.png", TargetType = typeof(MyFavoritePage) };
        Playing = new AppSection() { Title = "正在播放", Icon = "music.png", IconDark = "music_dark.png", TargetType = typeof(PlayingPage) };
        Settings = new AppSection() { Title = "我的", Icon = "my.png", IconDark = "my_dark.png", TargetType = typeof(SettingsPage) };
    }
}
