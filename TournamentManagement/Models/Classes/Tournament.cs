using System;
using System.Collections.Generic;
using System.ComponentModel;
using TournamentManagement.Models.Interfaces;

namespace TournamentManagement.Models.Classes;

public partial class Tournament : INotifyPropertyChanged, IDbItem
{
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
    public int Id { get; set; }
    public string? Name { get; set; }
}