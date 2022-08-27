﻿using ListenTogether.Business.Interfaces;
using ListenTogether.Model;
using ListenTogether.Model.Enums;
using ListenTogether.Network;

namespace ListenTogether.Business.Services;

public class MusicNetworkService : IMusicNetworkService
{
    private readonly MusicNetPlatform _musicNetPlatform;
    public MusicNetworkService(MusicNetPlatform musicNetPlatform)
    {
        _musicNetPlatform = musicNetPlatform;
    }

    public async Task<List<string>> GetSearchSuggest(string keyword)
    {
        return await _musicNetPlatform.GetSearchSuggest(keyword);
    }

    public async Task<List<MusicSearchResult>> Search(PlatformEnum platform, string keyword)
    {
        return await _musicNetPlatform.Search(platform, keyword);
    }

    public async Task<Music?> GetMusicDetail(MusicSearchResult musicSearchResult)
    {
        return await _musicNetPlatform.GetMusicDetail(musicSearchResult);
    }

    public async Task<Music?> UpdatePlayUrl(Music music)
    {
        return await _musicNetPlatform.UpdatePlayUrl(music);
    }

    public Task<string> GetMusicPlayPageUrl(Music music)
    {
        return _musicNetPlatform.GetMusicPlayPageUrl(music);
    }

    public async Task<List<string>> GetHotWord()
    {
        return await _musicNetPlatform.GetHotWord();
    }
}