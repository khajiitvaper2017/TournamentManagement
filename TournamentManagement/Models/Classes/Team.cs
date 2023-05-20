using System;
using System.Collections.Generic;
using System.ComponentModel;
using TournamentManagement.Models.Interfaces;

namespace TournamentManagement.Models.Classes;

public partial class Team : INotifyPropertyChanged, IDbItem
{
    public string? Country { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? Wins { get; set; }

    public int? Losses { get; set; }

    public virtual ICollection<Match> MatchTeam1s { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchTeam2s { get; set; } = new List<Match>();

    public virtual ICollection<TeamRoster> TeamRosters { get; set; } = new List<TeamRoster>();
    public int Id { get; set; }
    public string? Name { get; set; }
}