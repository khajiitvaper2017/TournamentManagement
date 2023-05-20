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
        LoadDatabase(connectionString: Settings.Default.ConnectionString);

        OpenTeamViewCommand = new RelayCommand(execute: _ => { new TeamWindow().ShowDialog(); }, canExecute: _ => IsConnected);
        OpenPlayerViewCommand = new RelayCommand(execute: _ => { new PlayerWindow().ShowDialog(); }, canExecute: _ => IsConnected);
        OpenTournamentViewCommand = new RelayCommand(execute: _ => { new TournamentWindow().ShowDialog(); }, canExecute: _ => IsConnected);
        OpenTeamRosterViewCommand = new RelayCommand(execute: _ => { new TeamRosterWindow().ShowDialog(); }, canExecute: _ => IsConnected);
        OpenMatchViewCommand = new RelayCommand(execute: _ => { new MatchWindow().ShowDialog(); }, canExecute: _ => IsConnected);
        OpenSettingsViewCommand = new RelayCommand(execute: _ => { new SettingsWindow().ShowDialog(); });
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
        DbTournamentContext = new DbTournamentContext(connectionString: connectionString);
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