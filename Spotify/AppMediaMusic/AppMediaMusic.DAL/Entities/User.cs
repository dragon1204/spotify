using System;
using System.Collections.Generic;

namespace AppMediaMusic.DAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

    public virtual UserProfile? UserProfile { get; set; }
}
