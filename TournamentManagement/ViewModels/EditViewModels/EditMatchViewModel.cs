using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using PropertyChanged;
using TournamentManagement.Models.Classes;
using TournamentManagement.Models.DbContext;
using TournamentManagement.Views;
using TournamentManagement.Views.EditWindows;

namespace TournamentManagement.ViewModels.EditViewModels;

public partial class EditMatchViewModel : INotifyPropertyChanged, IItem<Match>
{
    public EditMatchViewModel()
    {
        DbContext = MainViewModel.DbTournamentContext;

        EditCommand = new RelayCommand(execute: InsertItem, canExecute: _ => IsValidData);
        SelectTeam1Command = new RelayCommand(execute: obj =>
        {
            var win = new SelectItemWindow(items: DbContext.Teams.Where(predicate: t => t.Id != Team2Id).ToArray(),
                defaultItem: DbContext.Teams.FirstOrDefault(predicate: t => t.Id == Team1Id)){Owner = obj as Window };
            win.ShowDialog();
            if (win.DialogResult != true) return;

            Team1Id = (win.ReturnItem as Team)?.Id ?? default(int);
        });
        SelectTeam2Command = new RelayCommand(execute: obj =>
        {
            var win = new SelectItemWindow(items: DbContext.Teams.Where(predicate: t => t.Id != Team1Id).ToArray(),
                defaultItem: DbContext.Teams.FirstOrDefault(predicate: t => t.Id == Team2Id)){Owner = obj as Window };
            win.ShowDialog();
            if (win.DialogResult != true) return;
            Team2Id = (win.ReturnItem as Team)?.Id ?? default(int);
        });
        SelectTournamentCommand = new RelayCommand(execute: obj =>
        {
            var win = new SelectItemWindow(items: DbContext.Tournaments.ToArray(),
                defaultItem: DbContext.Tournaments.FirstOrDefault(predicate: t => t.Id == TournamentId))
            { Owner = obj as Window };
            win.ShowDialog();
            if (win.DialogResult != true) return;
            TournamentId = (win.ReturnItem as Tournament)?.Id ?? default(int);
        });
    }

    public RelayCommand EditCommand { get; set; }
    public RelayCommand SelectTeam1Command { get; set; }
    public RelayCommand SelectTeam2Command { get; set; }
    public RelayCommand SelectTournamentCommand { get; set; }
    protected DbTournamentContext DbContext { get; }
    public Match? Item { get; set; }
    [AlsoNotifyFor(property: "Team1Name")] public int Team1Id { get; set; }
    [AlsoNotifyFor(property: "Team2Name")] public int Team2Id { get; set; }
    [AlsoNotifyFor(property: "TournamentName")] public int TournamentId { get; set; }

    [AlsoNotifyFor(property: "ResultOptions")]
    public string Team1Name => DbContext.Teams.FirstOrDefault(predicate: t => t.Id == Team1Id)?.Name ?? string.Empty;

    [AlsoNotifyFor(property: "ResultOptions")]
    public string Team2Name => DbContext.Teams.FirstOrDefault(predicate: t => t.Id == Team2Id)?.Name ?? string.Empty;

    public string TournamentName =>
        DbContext.Tournaments.FirstOrDefault(predicate: t => t.Id == TournamentId)?.Name ?? string.Empty;

    public DateTime Date { get; set; } = DateTime.Now;
    public string Result { get; set; }
    public string Map { get; set; }
    public ObservableCollection<string> ResultOptions => new() { $"{Team1Name} Win", $"{Team2Name} Win" };

    public ObservableCollection<string> MapOptions => new()
    {
        "Dust II",
        "Inferno",
        "Mirage",
        "Nuke",
        "Overpass",
        "Cache",
        "Train"
    };
    

    protected bool IsValidData
    {
        get
        {
            if (DbContext.Teams.Find(keyValues: Team1Id) == null || DbContext.Teams.Find(keyValues: Team2Id) == null ||
                DbContext.Tournaments.Find(keyValues: TournamentId) == null)
                return false;
            if (DbContext.Tournaments.Find(keyValues: TournamentId)?.StartDate > Date ||
                DbContext.Tournaments.Find(keyValues: TournamentId)?.EndDate < Date)
                return false;
            if (string.IsNullOrEmpty(value: Result))
                return false;
            if (string.IsNullOrEmpty(value: Map)) 
                return false;
            return true;
        }
    }

    public void SetItem(Match? item)
    {
        if (item == null)
            return;
        Item = item;
        TournamentId = item.TournamentId ?? default(int);
        Team1Id = item.Team1Id ?? default(int);
        Team2Id = item.Team2Id ?? default(int);
        Date = item.Date ?? DateTime.Now;
        Result = item.Result;
        Map = item.Map;

        EditCommand = new RelayCommand(execute: EditItem, canExecute: _ => IsValidData);
    }

    protected void InsertItem(object obj)
    {
        DbContext.InsertMatch(tournamentId: TournamentId, team1Id: Team1Id, team2Id: Team2Id, matchDate: Date, matchResult: Result, map: Map);

        Close(window: obj as EditMatchWindow);
    }

    protected void EditItem(object obj)
    {
        DbContext.EditMatch(matchId: Item.Id, tournamentId: TournamentId, team1Id: Team1Id, team2Id: Team2Id, matchDate: Date, matchResult: Result, map: Map);

        Close(window: obj as EditMatchWindow);
    }

    protected void Close(Window? window)
    {
        window?.Close();
    }
}