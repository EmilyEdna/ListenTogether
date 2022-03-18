﻿using Microsoft.EntityFrameworkCore;
using MusicPlayerOnline.Api.Entities;

namespace MusicPlayerOnline.Api.DbContext
{
    public sealed class DataContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<UserConfigEntity> UserConfigs { get; set; } = null!;
        public DbSet<MusicEntity> Musics { get; set; } = null!;
        public DbSet<MyFavoriteEntity> MyFavorites { get; set; } = null!;
        public DbSet<PlaylistEntity> Playlists { get; set; } = null!;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlaylistEntity>().HasKey(x => new { x.UserBaseId, x.MusicId });
        }
    }
}
