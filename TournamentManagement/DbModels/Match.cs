using System;
using System.Collections.Generic;

namespace TournamentManagement.DbModels;

public partial class Match
{
    public int MatchId { get; set; }

    public int? TournamentId { get; set; }

    public int? Team1Id { get; set; }

    public int? Team2Id { get; set; }

    public DateTime? MatchDate { get; set; }

    public string? MatchResult { get; set; }

    public virtual Team? Team1 { get; set; }

    public virtual Team? Team2 { get; set; }

    public virtual Tournament? Tournament { get; set; }
}
