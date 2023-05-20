using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TournamentManagement.Models.Interfaces;

namespace TournamentManagement.Models.Classes;

public partial class Player : INotifyPropertyChanged, IDbItem
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Nickname { get; set; }

    public string? Country { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public virtual ICollection<TeamRoster> TeamRosters { get; set; } = new List<TeamRoster>();
    public int Id { get; set; }

    [NotMapped] public string Name => $"{FirstName} \"{Nickname}\" {LastName}";
}