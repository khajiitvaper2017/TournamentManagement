using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TournamentManagement.Models.Interfaces;

namespace TournamentManagement.Models.Classes;

public partial class TeamRoster : INotifyPropertyChanged, IDbItem
{
    public int? TeamId { get; set; }

    public int? PlayerId { get; set; }

    public string? PlayerPosition { get; set; }

    public virtual Player? Player { get; set; }

    public virtual Team? Team { get; set; }
    public int Id { get; set; }
    [NotMapped] public string Name => $"{Team?.Name} {Player?.Name}";
}