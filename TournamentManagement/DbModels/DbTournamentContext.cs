using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TournamentManagement.DbModels;

public partial class DbTournamentContext : DbContext
{
    public DbTournamentContext()
    {
    }

    public DbTournamentContext(DbContextOptions<DbTournamentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamRoster> TeamRosters { get; set; }

    public virtual DbSet<Tournament> Tournaments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=DB_Tournament;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.MatchId).HasName("PK__matches__9D7FCBA378FC71D5");

            entity.ToTable("matches");

            entity.Property(e => e.MatchId).HasColumnName("match_id");
            entity.Property(e => e.MatchDate)
                .HasColumnType("date")
                .HasColumnName("match_date");
            entity.Property(e => e.MatchResult)
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
            entity.HasKey(e => e.PlayerId).HasName("PK__players__44DA120C44FD3582");

            entity.ToTable("players");

            entity.Property(e => e.PlayerId).HasColumnName("player_id");
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
            entity.HasKey(e => e.TeamId).HasName("PK__teams__F82DEDBCF42A31B6");

            entity.ToTable("teams", tb => tb.HasTrigger("delete_matches_on_team_delete"));

            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.DateCreated)
                .HasColumnType("date")
                .HasColumnName("date_created");
            entity.Property(e => e.Losses).HasColumnName("losses");
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("team_name");
            entity.Property(e => e.Wins).HasColumnName("wins");
        });

        modelBuilder.Entity<TeamRoster>(entity =>
        {
            entity.HasKey(e => e.RosterId).HasName("PK__team_ros__B16836095094F503");

            entity.ToTable("team_roster");

            entity.Property(e => e.RosterId).HasColumnName("roster_id");
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
            entity.HasKey(e => e.TournamentId).HasName("PK__tourname__B93AA09D360E5272");

            entity.ToTable("tournaments");

            entity.Property(e => e.TournamentId).HasColumnName("tournament_id");
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
            entity.Property(e => e.TournamentName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tournament_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
