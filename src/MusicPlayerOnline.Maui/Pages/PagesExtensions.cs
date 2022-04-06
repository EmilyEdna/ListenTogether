﻿namespace MusicPlayerOnline.Maui.Pages;

public static class PagesExtensions
{
    public static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MyFavoritePage>();
        builder.Services.AddSingleton<PlayingPage>();
        builder.Services.AddSingleton<PlaylistPage>();
        builder.Services.AddTransient<SearchPage>();
        builder.Services.AddSingleton<SettingsPage>();

        builder.Services.AddTransient<MyFavoriteAddPage>();
        builder.Services.AddTransient<MyFavoriteDetailPage>();
        
        return builder;
    }
}
