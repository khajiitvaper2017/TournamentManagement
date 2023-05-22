using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TournamentManagement.Models.Interfaces;

namespace TournamentManagement.Models.Classes;

[Table("team_roster")]
public partial class TeamRoster : IDbItem, INotifyPropertyChanged
{
    [Column("team_id")] public int? TeamId { get; set; }

    [Column("player_id")] public int? PlayerId { get; set; }

    [Column("player_position")]
    [StringLength(50)]
    public string PlayerPosition { get; set; }

    public virtual Player Player { get; set; }

    public virtual Team Team { get; set; }

    [Column("roster_id")] [Key] public int Id { get; set; }

    [NotMapped] public string Name => $"{Team?.Name} {Player?.Name}";
}