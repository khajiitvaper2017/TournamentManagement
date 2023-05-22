using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TournamentManagement.Models.Interfaces;

namespace TournamentManagement.Models.Classes;

public partial class Team : IDbItem, INotifyPropertyChanged
{
    [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Team()
    {
        Matches1 = new HashSet<Match>();
        Matches2 = new HashSet<Match>();
        TeamRoster = new HashSet<TeamRoster>();
    }

    [StringLength(50)] public string Country { get; set; }

    [Column("date_created", TypeName = "date")]
    public DateTime? DateCreated { get; set; }

    public int? Wins { get; set; }

    public int? Losses { get; set; }

    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Match> Matches1 { get; set; }

    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Match> Matches2 { get; set; }

    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<TeamRoster> TeamRoster { get; set; }

    [Column("team_id")] [Key] public int Id { get; set; }

    [Column("team_name")]
    [StringLength(50)]
    public string Name { get; set; }
}