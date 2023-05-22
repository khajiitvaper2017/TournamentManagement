using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TournamentManagement.Models.Classes;
using TournamentManagement.Properties;

namespace TournamentManagement.Models.DbContext;

public class DbTournamentContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbTournamentContext()
    {
        ConnectionString = Settings.Default.ConnectionString;
    }

    public DbTournamentContext(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public DbTournamentContext(DbContextOptions<DbTournamentContext> options)
        : base(options)
    {
    }

    public string ConnectionString { get; set; }

    public virtual DbSet<Match> Matches { get; set; }
    public virtual DbSet<Player> Players { get; set; }
    public virtual DbSet<TeamRoster> TeamRosters { get; set; }
    public virtual DbSet<Team> Teams { get; set; }
    public virtual DbSet<Tournament> Tournaments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>()
            .Property(e => e.Result)
            .IsUnicode(false);

        modelBuilder.Entity<Match>()
            .Property(e => e.Map)
            .IsUnicode(false);

        modelBuilder.Entity<Player>()
            .Property(e => e.FirstName)
            .IsUnicode(false);

        modelBuilder.Entity<Player>()
            .Property(e => e.LastName)
            .IsUnicode(false);

        modelBuilder.Entity<Player>()
            .Property(e => e.Nickname)
            .IsUnicode(false);

        modelBuilder.Entity<Player>()
            .Property(e => e.Country)
            .IsUnicode(false);

        modelBuilder.Entity<Player>()
            .HasMany(e => e.TeamRoster)
            .WithOne(e => e.Player)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TeamRoster>()
            .Property(e => e.PlayerPosition)
            .IsUnicode(false);

        modelBuilder.Entity<Team>()
            .Property(e => e.Name)
            .IsUnicode(false);

        modelBuilder.Entity<Team>()
            .Property(e => e.Country)
            .IsUnicode(false);

        modelBuilder.Entity<Team>()
            .HasMany(e => e.Matches1)
            .WithOne(e => e.Team1)
            .HasForeignKey(e => e.Team1Id)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Team>()
            .HasMany(e => e.Matches2)
            .WithOne(e => e.Team2)
            .HasForeignKey(e => e.Team2Id)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Team>()
            .HasMany(e => e.TeamRoster)
            .WithOne(e => e.Team)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Tournament>()
            .Property(e => e.Name)
            .IsUnicode(false);

        modelBuilder.Entity<Tournament>()
            .Property(e => e.Location)
            .IsUnicode(false);

        modelBuilder.Entity<Tournament>()
            .HasMany(e => e.Matches)
            .WithOne(e => e.Tournament)
            .OnDelete(DeleteBehavior.Cascade);
    }


    public void InsertTeam(string teamName, string country, DateTime dateCreated, int wins, int losses)
    {
        Teams.Add(new Team
        {
            Name = teamName,
            Country = country,
            DateCreated = dateCreated,
            Wins = wins,
            Losses = losses
        });
        SaveChanges();
    }

    public void EditTeam(int teamId, string teamName, string country, DateTime dateCreated, int wins, int losses)
    {
        var team = Teams.Find(keyValues: teamId);
        if (team == null) return;

        team.Name = teamName;
        team.Country = country;
        team.DateCreated = dateCreated;
        team.Wins = wins;
        team.Losses = losses;
        SaveChanges();
    }

    public void DeleteTeam(int teamId)
    {
        var team = Teams.Find(keyValues: teamId);
        if (team == null) return;

        Matches.RemoveRange(Matches.Where(m => m.Team1Id == teamId || m.Team2Id == teamId));
        TeamRosters.RemoveRange(TeamRosters.Where(tr => tr.TeamId == teamId));

        Teams.Remove(team);

        SaveChanges();
    }

    public void InsertPlayer(string firstName, string lastName, string nickname, string country, DateTime dateOfBirth)
    {
        Players.Add(new Player
        {
            FirstName = firstName,
            LastName = lastName,
            Nickname = nickname,
            Country = country,
            DateOfBirth = dateOfBirth
        });
        SaveChanges();
    }

    public void EditPlayer(int playerId, string firstName, string lastName, string nickname, string country,
        DateTime dateOfBirth)
    {
        var player = Players.Find(keyValues: playerId);
        if (player == null) return;
        player.FirstName = firstName;
        player.LastName = lastName;
        player.Nickname = nickname;
        player.Country = country;
        player.DateOfBirth = dateOfBirth;
        SaveChanges();
    }

    public void DeletePlayer(int playerId)
    {
        var player = Players.Find(keyValues: playerId);
        if (player == null) return;
        TeamRosters.RemoveRange(TeamRosters.Where(tr => tr.PlayerId == playerId));
        Players.Remove(player);
        SaveChanges();
    }

    public void InsertTournament(string tournamentName, DateTime startDate, DateTime endDate, string location)
    {
        Tournaments.Add(new Tournament
        {
            Name = tournamentName,
            StartDate = startDate,
            EndDate = endDate,
            Location = location
        });
        SaveChanges();
    }

    public void EditTournament(int tournamentId, string tournamentName, DateTime startDate, DateTime endDate,
        string location)
    {
        var tournament = Tournaments.Find(keyValues: tournamentId);
        if (tournament == null) return;
        tournament.Name = tournamentName;
        tournament.StartDate = startDate;
        tournament.EndDate = endDate;
        tournament.Location = location;
        SaveChanges();
    }

    public void DeleteTournament(int tournamentId)
    {
        var tournament = Tournaments.Find(keyValues: tournamentId);
        if (tournament == null) return;
        Matches.RemoveRange(Matches.Where(m => m.TournamentId == tournamentId));
        Tournaments.Remove(tournament);
        SaveChanges();
    }

    public void InsertTeamRoster(int playerId, int teamId, string playerPosition)
    {
        if (TeamRosters.Any(tr => tr.PlayerId == playerId)) return;

        TeamRosters.Add(new TeamRoster
        {
            TeamId = teamId,
            PlayerId = playerId,
            PlayerPosition = playerPosition
        });
        SaveChanges();
    }

    public void EditTeamRoster(int rosterId, int playerId, int teamId, string playerPosition)
    {
        var roster = TeamRosters.Find(keyValues: rosterId);
        if (roster == null) return;
        roster.TeamId = teamId;
        roster.PlayerId = playerId;
        roster.PlayerPosition = playerPosition;
        SaveChanges();
    }

    public void DeleteTeamRoster(int rosterId)
    {
        var roster = TeamRosters.Find(keyValues: rosterId);
        if (roster == null) return;
        TeamRosters.Remove(roster);
        SaveChanges();
    }

    public void InsertMatch(int tournamentId, int team1Id, int team2Id, DateTime matchDate, string matchResult,
        string map)
    {
        Matches.Add(new Match
        {
            TournamentId = tournamentId,
            Team1Id = team1Id,
            Team2Id = team2Id,
            Date = matchDate,
            Result = matchResult,
            Map = map
        });

        var team1 = Teams.Find(keyValues: team1Id);
        var team2 = Teams.Find(keyValues: team2Id);

        if (matchResult == $"{team1.Name} Win")
        {
            team1.Wins++;
            team2.Losses++;
        }
        else if (matchResult == $"{team2.Name} Win")
        {
            team2.Wins++;
            team1.Losses++;
        }

        SaveChanges();
    }

    public void EditMatch(int matchId, int tournamentId, int team1Id, int team2Id, DateTime matchDate,
        string matchResult, string map)
    {
        var match = Matches.Find(keyValues: matchId);
        if (match == null) return;

        if (match.Result == $"{match.Team1.Name} Win")
        {
            match.Team1.Wins--;
            match.Team2.Losses--;
        }
        else if (match.Result == $"{match.Team2.Name} Win")
        {
            match.Team2.Wins--;
            match.Team1.Losses--;
        }

        match.TournamentId = tournamentId;
        match.Team1Id = team1Id;
        match.Team2Id = team2Id;
        match.Date = matchDate;
        match.Result = matchResult;
        match.Map = map;

        if (matchResult == $"{match.Team1.Name} Win")
        {
            match.Team1.Wins++;
            match.Team2.Losses++;
        }
        else if (matchResult == $"{match.Team2.Name} Win")
        {
            match.Team2.Wins++;
            match.Team1.Losses++;
        }

        SaveChanges();
    }

    public void DeleteMatch(int matchId)
    {
        var match = Matches.Find(keyValues: matchId);
        if (match == null) return;

        if (match.Result == $"{match.Team1.Name} Win")
        {
            match.Team1.Wins--;
            match.Team2.Losses--;
        }
        else if (match.Result == $"{match.Team2.Name} Win")
        {
            match.Team2.Wins--;
            match.Team1.Losses--;
        }

        Matches.Remove(match);
        SaveChanges();
    }
}