using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TournamentManagement.Models.Interfaces;

namespace TournamentManagement.Models.Classes;

public partial class Match : INotifyPropertyChanged, IDbItem
{
    public int? TournamentId { get; set; }

    public int? Team1Id { get; set; }

    public int? Team2Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Result { get; set; }

    public virtual Team? Team1 { get; set; }

    public virtual Team? Team2 { get; set; }

    public virtual Tournament? Tournament { get; set; }
    public int Id { get; set; }
    [NotMapped] public string Name => $"{Tournament?.Name} {Id}. {Team1?.Name} vs. {Team2?.Name}: Result: {Result}";
}