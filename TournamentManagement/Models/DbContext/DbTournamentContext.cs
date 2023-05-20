using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TournamentManagement.Models.Classes;

namespace TournamentManagement.Models.DbContext;

public partial class DbTournamentContext : Microsoft.EntityFrameworkCore.DbContext, INotifyPropertyChanged
{
    public DbTournamentContext(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public DbTournamentContext(DbContextOptions<DbTournamentContext> options)
        : base(options)
    {
    }

    private string ConnectionString { get; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Player> Players { get; set; }


    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamRoster> TeamRosters { get; set; }

    public virtual DbSet<Tournament> Tournaments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
        optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Warning);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__matches__9D7FCBA378FC71D5");

            entity.ToTable("matches");

            entity.Property(e => e.Id).HasColumnName("match_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("match_date");
            entity.Property(e => e.Result)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("match_result");
            entity.Property(e => e.Team1Id).HasColumnName("team1_id");
            entity.Property(e => e.Team2Id).HasColumnName("team2_id");
            entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

            entity.HasOne(d => d.Team1).WithMany(p => p.MatchTeam1s)
                .HasForeignKey(d => d.Team1Id)
                .HasConstraintName("FK__matches__team1_i__5441852A");

            entity.HasOne(d => d.Team2).WithMany(p => p.MatchTeam2s)
                .HasForeignKey(d => d.Team2Id)
                .HasConstraintName("FK__matches__team2_i__5535A963");

            entity.HasOne(d => d.Tournament).WithMany(p => p.Matches)
                .HasForeignKey(d => d.TournamentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__matches__tournam__534D60F1");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__players__44DA120C44FD3582");

            entity.ToTable("players");

            entity.Property(e => e.Id).HasColumnName("player_id");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nickname");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__teams__F82DEDBCF42A31B6");

            entity.ToTable("teams", tb => tb.HasTrigger("delete_matches_on_team_delete"));

            entity.Property(e => e.Id).HasColumnName("team_id");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.DateCreated)
                .HasColumnType("date")
                .HasColumnName("date_created");
            entity.Property(e => e.Losses).HasColumnName("losses");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("team_name");
            entity.Property(e => e.Wins).HasColumnName("wins");
        });

        modelBuilder.Entity<TeamRoster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__team_ros__B16836095094F503");

            entity.ToTable("team_roster");

            entity.Property(e => e.Id).HasColumnName("roster_id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.PlayerPosition)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("player_position");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Player).WithMany(p => p.TeamRosters)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__team_rost__playe__5070F446");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamRosters)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__team_rost__team___4F7CD00D");
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tourname__B93AA09D360E5272");

            entity.ToTable("tournaments");

            entity.Property(e => e.Id).HasColumnName("tournament_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tournament_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

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
        var team = Teams.Find(teamId);
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
        var team = Teams.Find(teamId);
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
        var player = Players.Find(playerId);
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
        var player = Players.Find(playerId);
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
        var tournament = Tournaments.Find(tournamentId);
        if (tournament == null) return;
        tournament.Name = tournamentName;
        tournament.StartDate = startDate;
        tournament.EndDate = endDate;
        tournament.Location = location;
        SaveChanges();
    }

    public void DeleteTournament(int tournamentId)
    {
        var tournament = Tournaments.Find(tournamentId);
        if (tournament == null) return;
        Matches.RemoveRange(Matches.Where(m => m.TournamentId == tournamentId));
        Tournaments.Remove(tournament);
        SaveChanges();
    }

    public void InsertTeamRoster(int teamId, int playerId, string playerPosition)
    {
        TeamRosters.Add(new TeamRoster
        {
            TeamId = teamId,
            PlayerId = playerId,
            PlayerPosition = playerPosition
        });
        SaveChanges();
    }

    public void EditTeamRoster(int rosterId, int teamId, int playerId, string playerPosition)
    {
        var roster = TeamRosters.Find(rosterId);
        if (roster == null) return;
        roster.TeamId = teamId;
        roster.PlayerId = playerId;
        roster.PlayerPosition = playerPosition;
        SaveChanges();
    }

    public void DeleteTeamRoster(int rosterId)
    {
        var roster = TeamRosters.Find(rosterId);
        if (roster == null) return;
        TeamRosters.Remove(roster);
        SaveChanges();
    }

    public void InsertMatch(int tournamentId, int team1Id, int team2Id, DateTime matchDate, string matchResult)
    {
        Matches.Add(new Match
        {
            TournamentId = tournamentId,
            Team1Id = team1Id,
            Team2Id = team2Id,
            Date = matchDate,
            Result = matchResult
        });
        SaveChanges();
    }

    public void EditMatch(int matchId, int tournamentId, int team1Id, int team2Id, DateTime matchDate,
        string matchResult)
    {
        var match = Matches.Find(matchId);
        if (match == null) return;
        match.TournamentId = tournamentId;
        match.Team1Id = team1Id;
        match.Team2Id = team2Id;
        match.Date = matchDate;
        match.Result = matchResult;
        SaveChanges();
    }

    public void DeleteMatch(int matchId)
    {
        var match = Matches.Find(matchId);
        if (match == null) return;
        Matches.Remove(match);
        SaveChanges();
    }
}