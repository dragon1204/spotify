using System;
using System.Collections.Generic;

namespace AppMediaMusic.DAL.Entities;

public partial class PlaylistSong
{
    public int PlaylistsongId { get; set; }

    public int PlaylistId { get; set; }

    public int SongId { get; set; }

    public DateTime? AddedAt { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;

    public virtual Song Song { get; set; } = null!;
}
