using System;
using System.Collections.Generic;

namespace AppMediaMusic.DAL.Entities;

public partial class UserProfile
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly Birth { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public virtual User User { get; set; } = null!;
}
