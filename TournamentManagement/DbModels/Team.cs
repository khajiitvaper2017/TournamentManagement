using System;
using System.Collections.Generic;

namespace TournamentManagement.DbModels;

public partial class Team
{
    public int TeamId { get; set; }

    public string? TeamName { get; set; }

    public string? Country { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? Wins { get; set; }

    public int? Losses { get; set; }

    public virtual ICollection<Match> MatchTeam1s { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchTeam2s { get; set; } = new List<Match>();

    public virtual ICollection<TeamRoster> TeamRosters { get; set; } = new List<TeamRoster>();
}
