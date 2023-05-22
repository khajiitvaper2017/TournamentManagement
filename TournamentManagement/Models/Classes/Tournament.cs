using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TournamentManagement.Models.Interfaces;

namespace TournamentManagement.Models.Classes;

public partial class Tournament : IDbItem, INotifyPropertyChanged
{
    public Tournament()
    {
        Matches = new HashSet<Match>();
    }

    [Column("start_date", TypeName = "date")]
    public DateTime? StartDate { get; set; }

    [Column("end_date", TypeName = "date")]
    public DateTime? EndDate { get; set; }

    [StringLength(50)] public string Location { get; set; }
    
    public virtual ICollection<Match> Matches { get; set; }

    [Column("tournament_id")] [Key] public int Id { get; set; }

    [Column("tournament_name")]
    [StringLength(50)]
    public string Name { get; set; }
}