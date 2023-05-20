using System;
using System.Collections.Generic;

namespace TournamentManagement.DbModels;

public partial class Tournament
{
    public int TournamentId { get; set; }

    public string? TournamentName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
