using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TournamentManagement.Models.Interfaces;

namespace TournamentManagement.Models.Classes;

public partial class Player : IDbItem, INotifyPropertyChanged
{
    public Player()
    {
        TeamRoster = new HashSet<TeamRoster>();
    }

    [Column("first_name")]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Column("last_name")]
    [StringLength(50)]
    public string LastName { get; set; }

    [StringLength(50)] public string Nickname { get; set; }

    [StringLength(50)] public string Country { get; set; }

    [Column("date_of_birth", TypeName = "date")]
    public DateTime? DateOfBirth { get; set; }
    
    public virtual ICollection<TeamRoster> TeamRoster { get; set; }

    [Column("player_id")] [Key] public int Id { get; set; }

    [NotMapped] public string Name => $"{FirstName} \"{Nickname}\" {LastName}";
}