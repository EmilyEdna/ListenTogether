﻿namespace ListenTogether.Filters.MusicSearchFilter;
internal class VipMusicFilter : IMusicSearchFilter
{
    public List<MusicSearchResult> Filter(List<MusicSearchResult> musics)
    {
        return musics.Where(x => x.Fee != Model.Enums.FeeEnum.Vip).ToList();
    }
}