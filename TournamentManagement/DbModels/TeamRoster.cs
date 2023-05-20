using System;
using System.Collections.Generic;

namespace TournamentManagement.DbModels;

public partial class TeamRoster
{
    public int RosterId { get; set; }

    public int? TeamId { get; set; }

    public int? PlayerId { get; set; }

    public string? PlayerPosition { get; set; }

    public virtual Player? Player { get; set; }

    public virtual Team? Team { get; set; }
}
