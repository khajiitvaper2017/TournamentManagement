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
    public DbTournamentContext()
    {
        ConnectionString = Properties.Settings.Default.ConnectionString;
    }
    public DbTournamentContext(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public DbTournamentContext(DbContextOptions<DbTournamentContext> options)
        : base(options: options)
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
        optionsBuilder.UseSqlServer(connectionString: ConnectionString);
        optionsBuilder.LogTo(action: message => Debug.WriteLine(message: message), minimumLevel: LogLevel.Warning);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>(buildAction: entity =>
        {
            entity.HasKey(keyExpression: e => e.Id).HasName(name: "PK__matches__9D7FCBA378FC71D5");

            entity.ToTable(name: "matches");

            entity.Property(propertyExpression: e => e.Id).HasColumnName(name: "match_id");
            entity.Property(propertyExpression: e => e.Date)
                .HasColumnType(typeName: "date")
                .HasColumnName(name: "match_date");
            entity.Property(propertyExpression: e => e.Result)
                .HasMaxLength(maxLength: 50)
                .IsUnicode(unicode: false)
                .HasColumnName(name: "match_result");
            entity.Property(propertyExpression: e => e.Map)
                .HasMaxLength(maxLength: 50)
                .IsUnicode(unicode: false)
                .HasColumnName(name: "match_map");
            entity.Property(propertyExpression: e => e.Team1Id).HasColumnName(name: "team1_id");
            entity.Property(propertyExpression: e => e.Team2Id).HasColumnName(name: "team2_id");
            entity.Property(propertyExpression: e => e.TournamentId).HasColumnName(name: "tournament_id");

            entity.HasOne(navigationExpression: d => d.Team1).WithMany(navigationExpression: p => p.MatchTeam1s)
                .HasForeignKey(foreignKeyExpression: d => d.Team1Id)
                .HasConstraintName(name: "FK__matches__team1_i__5441852A");

            entity.HasOne(navigationExpression: d => d.Team2).WithMany(navigationExpression: p => p.MatchTeam2s)
                .HasForeignKey(foreignKeyExpression: d => d.Team2Id)
                .HasConstraintName(name: "FK__matches__team2_i__5535A963");

            entity.HasOne(navigationExpression: d => d.Tournament).WithMany(navigationExpression: p => p.Matches)
                .HasForeignKey(foreignKeyExpression: d => d.TournamentId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade)
                .HasConstraintName(name: "FK__matches__tournam__534D60F1");
        });

        modelBuilder.Entity<Player>(buildAction: entity =>
        {
            entity.HasKey(keyExpression: e => e.Id).HasName(name: "PK__players__44DA120C44FD3582");

            entity.ToTable(name: "players");

            entity.Property(propertyExpression: e => e.Id).HasColumnName(name: "player_id");
            entity.Property(propertyExpression: e => e.Country)
                .HasMaxLength(maxLength: 50)
                .IsUnicode(unicode: false)
                .HasColumnName(name: "country");
            entity.Property(propertyExpression: e => e.DateOfBirth)
                .HasColumnType(typeName: "date")
                .HasColumnName(name: "date_of_birth");
            entity.Property(propertyExpression: e => e.FirstName)
                .HasMaxLength(maxLength: 50)
                .IsUnicode(unicode: false)
                .HasColumnName(name: "first_name");
            entity.Property(propertyExpression: e => e.LastName)
                .HasMaxLength(maxLength: 50)
                .IsUnicode(unicode: false)
                .HasColumnName(name: "last_name");
            entity.Property(propertyExpression: e => e.Nickname)
                .HasMaxLength(maxLength: 50)
                .IsUnicode(unicode: false)
                .HasColumnName(name: "nickname");
        });

        modelBuilder.Entity<Team>(buildAction: entity =>
        {
            entity.HasKey(keyExpression: e => e.Id).HasName(name: "PK__teams__F82DEDBCF42A31B6");

            entity.ToTable(name: "teams", buildAction: tb => tb.HasTrigger(modelName: "delete_matches_on_team_delete"));

            entity.Property(propertyExpression: e => e.Id).HasColumnName(name: "team_id");
            entity.Property(propertyExpression: e => e.Country)
                .HasMaxLength(maxLength: 50)
                .IsUnicode(unicode: false)
                .HasColumnName(name: "country");
            entity.Property(propertyExpression: e => e.DateCreated)
                .HasColumnType(typeName: "date")
                .HasColumnName(name: "date_created");
            entity.Property(propertyExpression: e => e.Losses).HasColumnName(name: "losses");
            entity.Property(propertyExpression: e => e.Name)
                .HasMaxLength(maxLength: 50)
                .IsUnicode(unicode: false)
                .HasColumnName(name: "team_name");
            entity.Property(propertyExpression: e => e.Wins).HasColumnName(name: "wins");
        });

        modelBuilder.Entity<TeamRoster>(buildAction: entity =>
        {
            entity.HasKey(keyExpression: e => e.Id).HasName(name: "PK__team_ros__B16836095094F503");

            entity.ToTable(name: "team_roster");

            entity.Property(propertyExpression: e => e.Id).HasColumnName(name: "roster_id");
            entity.Property(propertyExpression: e => e.PlayerId).HasColumnName(name: "player_id");
            entity.Property(propertyExpression: e => e.PlayerPosition)
                .HasMaxLength(maxLength: 50)
                .IsUnicode(unicode: false)
                .HasColumnName(name: "player_position");
            entity.Property(propertyExpression: e => e.TeamId).HasColumnName(name: "team_id");

            entity.HasOne(navigationExpression: d => d.Player).WithMany(navigationExpression: p => p.TeamRosters)
                .HasForeignKey(foreignKeyExpression: d => d.PlayerId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade)
                .HasConstraintName(name: "FK__team_rost__playe__5070F446");

            entity.HasOne(navigationExpression: d => d.Team).WithMany(navigationExpression: p => p.TeamRosters)
                .HasForeignKey(foreignKeyExpression: d => d.TeamId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade)
                .HasConstraintName(name: "FK__team_rost__team___4F7CD00D");
        });

        modelBuilder.Entity<Tournament>(buildAction: entity =>
        {
            entity.HasKey(keyExpression: e => e.Id).HasName(name: "PK__tourname__B93AA09D360E5272");

            entity.ToTable(name: "tournaments");

            entity.Property(propertyExpression: e => e.Id).HasColumnName(name: "tournament_id");
            entity.Property(propertyExpression: e => e.EndDate)
                .HasColumnType(typeName: "date")
                .HasColumnName(name: "end_date");
            entity.Property(propertyExpression: e => e.Location)
                .HasMaxLength(maxLength: 50)
                .IsUnicode(unicode: false)
                .HasColumnName(name: "location");
            entity.Property(propertyExpression: e => e.StartDate)
                .HasColumnType(typeName: "date")
                .HasColumnName(name: "start_date");
            entity.Property(propertyExpression: e => e.Name)
                .HasMaxLength(maxLength: 50)
                .IsUnicode(unicode: false)
                .HasColumnName(name: "tournament_name");
        });

        OnModelCreatingPartial(modelBuilder: modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public void InsertTeam(string teamName, string country, DateTime dateCreated, int wins, int losses)
    {
        Teams.Add(entity: new Team
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

        Matches.RemoveRange(entities: Matches.Where(predicate: m => m.Team1Id == teamId || m.Team2Id == teamId));
        TeamRosters.RemoveRange(entities: TeamRosters.Where(predicate: tr => tr.TeamId == teamId));

        Teams.Remove(entity: team);

        SaveChanges();
    }

    public void InsertPlayer(string firstName, string lastName, string nickname, string country, DateTime dateOfBirth)
    {
        Players.Add(entity: new Player
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
        TeamRosters.RemoveRange(entities: TeamRosters.Where(predicate: tr => tr.PlayerId == playerId));
        Players.Remove(entity: player);
        SaveChanges();
    }

    public void InsertTournament(string tournamentName, DateTime startDate, DateTime endDate, string location)
    {
        Tournaments.Add(entity: new Tournament
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
        Matches.RemoveRange(entities: Matches.Where(predicate: m => m.TournamentId == tournamentId));
        Tournaments.Remove(entity: tournament);
        SaveChanges();
    }

    public void InsertTeamRoster(int playerId, int teamId, string playerPosition)
    {
        if(TeamRosters.Any(predicate: tr => tr.PlayerId == playerId)) return;

        TeamRosters.Add(entity: new TeamRoster
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
        TeamRosters.Remove(entity: roster);
        SaveChanges();
    }

    public void InsertMatch(int tournamentId, int team1Id, int team2Id, DateTime matchDate, string matchResult, string map)
    {
        Matches.Add(entity: new Match
        {
            TournamentId = tournamentId,
            Team1Id = team1Id,
            Team2Id = team2Id,
            Date = matchDate,
            Result = matchResult,
            Map = map
        });
        SaveChanges();
    }

    public void EditMatch(int matchId, int tournamentId, int team1Id, int team2Id, DateTime matchDate,
        string matchResult, string map)
    {
        var match = Matches.Find(keyValues: matchId);
        if (match == null) return;
        match.TournamentId = tournamentId;
        match.Team1Id = team1Id;
        match.Team2Id = team2Id;
        match.Date = matchDate;
        match.Result = matchResult;
        match.Map = map;
        SaveChanges();
    }

    public void DeleteMatch(int matchId)
    {
        var match = Matches.Find(keyValues: matchId);
        if (match == null) return;
        Matches.Remove(entity: match);
        SaveChanges();
    }
}