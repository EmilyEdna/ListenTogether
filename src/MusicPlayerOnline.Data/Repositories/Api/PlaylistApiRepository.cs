﻿using MusicPlayerOnline.Data.Interfaces;
using MusicPlayerOnline.Model;
using MusicPlayerOnline.Model.Api;
using MusicPlayerOnline.Model.Api.Request;
using MusicPlayerOnline.Model.Api.Response;

namespace MusicPlayerOnline.Data.Repositories.Api;
public class PlaylistApiRepository : IPlaylistRepository
{
    public async Task<bool> AddOrUpdateAsync(Playlist playlist)
    {
        var requestPlaylist = new PlaylistRequest()
        {
            MusicId = playlist.MusicId,
            MusicName = playlist.MusicName,
            MusicArtist = playlist.MusicArtist
        };

        string content = requestPlaylist.ToJson();
        StringContent sc = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
        var response = await DataConfig.HttpClientWithToken.PostAsync(DataConfig.ApiSetting.Playlist.AddOrUpdate, sc);
        var json = await response.Content.ReadAsStringAsync();
        var obj = json.ToObject<Result>();
        if (obj == null || obj.Code != 0)
        {
            return false;
        }
        return true;
    }

    public async Task<List<Playlist>?> GetAllAsync()
    {
        var json = await DataConfig.HttpClientWithToken.GetStringAsync(DataConfig.ApiSetting.Playlist.GetAll);
        var obj = json.ToObject<List<PlaylistResponse>>();
        if (obj == null)
        {
            return default;
        }

        return obj.Select(x => new Playlist()
        {
            Id = x.Id,
            MusicId = x.MusicId,
            MusicArtist = x.MusicArtist,
            MusicName = x.MusicName
        }).ToList();
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var url = string.Format(DataConfig.ApiSetting.Playlist.Remove, id);
        var response = await DataConfig.HttpClientWithToken.PostAsync(url, null);
        var json = await response.Content.ReadAsStringAsync();
        var obj = json.ToObject<Result>();
        if (obj == null || obj.Code != 0)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveAllAsync()
    {
        var response = await DataConfig.HttpClientWithToken.PostAsync(DataConfig.ApiSetting.Playlist.RemoveAll, null);
        var json = await response.Content.ReadAsStringAsync();
        var obj = json.ToObject<Result>();
        if (obj == null || obj.Code != 0)
        {
            return false;
        }
        return true;
    }
}