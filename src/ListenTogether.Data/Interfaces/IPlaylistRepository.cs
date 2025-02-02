﻿using ListenTogether.Model;

namespace ListenTogether.Data.Interfaces;
public interface IPlaylistRepository
{
    Task<bool> AddOrUpdateAsync(Playlist playlist);
    Task<List<Playlist>?> GetAllAsync();
    Task<bool> RemoveAsync(int id);
    Task<bool> RemoveAllAsync();
}