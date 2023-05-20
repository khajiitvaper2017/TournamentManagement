using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using TournamentManagement.Models.Classes;
using TournamentManagement.Models.DbContext;
using TournamentManagement.Properties;
using TournamentManagement.Views;
using TournamentManagement.Views.TableWindows;

namespace TournamentManagement.ViewModels;

public partial class MainViewModel : INotifyPropertyChanged
{
    public MainViewModel()
    {
        LoadDatabase(Settings.Default.ConnectionString);

        OpenTeamViewCommand = new RelayCommand(_ => { new TeamWindow().ShowDialog(); }, _ => IsConnected);
        OpenPlayerViewCommand = new RelayCommand(_ => { new PlayerWindow().ShowDialog(); }, _ => IsConnected);
        OpenTournamentViewCommand = new RelayCommand(_ => { new TournamentWindow().ShowDialog(); }, _ => IsConnected);
        OpenTeamRosterViewCommand = new RelayCommand(_ => { new TeamRosterWindow().ShowDialog(); }, _ => IsConnected);
        OpenMatchViewCommand = new RelayCommand(_ => { new MatchWindow().ShowDialog(); }, _ => IsConnected);
        OpenSettingsViewCommand = new RelayCommand(_ => { new SettingsWindow().ShowDialog(); });
    }

    public static bool IsConnected { get; set; }
    public bool IsShowAllTables { get; set; }
    public static string StatusText { get; set; }
    public static DbTournamentContext DbTournamentContext { get; set; }

    public static ObservableCollection<Team> Teams { get; set; }

    public static ObservableCollection<Player> Players { get; set; }

    public static ObservableCollection<Tournament> Tournaments { get; set; }

    public static ObservableCollection<Match> Matches { get; set; }

    public static ObservableCollection<TeamRoster> TeamRosters { get; set; }

    public RelayCommand OpenTeamViewCommand { get; }
    public RelayCommand OpenPlayerViewCommand { get; }
    public RelayCommand OpenTournamentViewCommand { get; }
    public RelayCommand OpenMatchViewCommand { get; }
    public RelayCommand OpenTeamRosterViewCommand { get; }
    public RelayCommand OpenSettingsViewCommand { get; }

    public static void LoadDatabase(string connectionString)
    {
        DbTournamentContext = new DbTournamentContext(connectionString);
        IsConnected = DbTournamentContext.Database.CanConnect();
        StatusText = IsConnected ? "Successfully connected to database." : "Failed to connect to database.";
        if (!IsConnected) return;

        DbTournamentContext.Teams.Load();
        DbTournamentContext.Players.Load();
        DbTournamentContext.Tournaments.Load();
        DbTournamentContext.Matches.Load();
        DbTournamentContext.TeamRosters.Load();

        Teams = DbTournamentContext.Teams.Local.ToObservableCollection();
        Players = DbTournamentContext.Players.Local.ToObservableCollection();
        Tournaments = DbTournamentContext.Tournaments.Local.ToObservableCollection();
        Matches = DbTournamentContext.Matches.Local.ToObservableCollection();
        TeamRosters = DbTournamentContext.TeamRosters.Local.ToObservableCollection();
    }
}