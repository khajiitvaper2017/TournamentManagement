using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TournamentManagement.Models.Interfaces;

namespace TournamentManagement.Models.Classes;

[Table("matches")]
public partial class Match : IDbItem, INotifyPropertyChanged
{
    [Column("tournament_id")] public int? TournamentId { get; set; }

    [Column("team1_id")] public int? Team1Id { get; set; }

    [Column("team2_id")] public int? Team2Id { get; set; }

    [Column("match_date", TypeName = "date")]
    public DateTime? Date { get; set; }

    [Column("match_result")]
    [StringLength(50)]
    public string Result { get; set; }

    [Column("match_map")]
    [StringLength(50)]
    public string Map { get; set; }

    public virtual Team Team1 { get; set; }

    public virtual Team Team2 { get; set; }

    public virtual Tournament Tournament { get; set; }

    [Column("match_id")] [Key] public int Id { get; set; }

    [Column("match_name")]
    [NotMapped]
    public string Name => $"{Tournament?.Name} {Id}. " +
                          $"{Team1?.Name} vs. {Team2?.Name}: " +
                          $"Result: {Result}";
}