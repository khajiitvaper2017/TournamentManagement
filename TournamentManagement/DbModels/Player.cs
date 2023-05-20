using System;
using System.Collections.Generic;

namespace TournamentManagement.DbModels;

public partial class Player
{
    public int PlayerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Nickname { get; set; }

    public string? Country { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public virtual ICollection<TeamRoster> TeamRosters { get; set; } = new List<TeamRoster>();
}
