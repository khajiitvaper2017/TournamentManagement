using System.ComponentModel;
using System.Linq;
using System.Windows;
using PropertyChanged;
using TournamentManagement.Models.Classes;
using TournamentManagement.Models.DbContext;
using TournamentManagement.Views;
using TournamentManagement.Views.EditWindows;

namespace TournamentManagement.ViewModels.EditViewModels;

public partial class EditTeamRosterViewModel : INotifyPropertyChanged
{
    public EditTeamRosterViewModel()
    {
        DbContext = MainViewModel.DbTournamentContext;

        EditCommand = new RelayCommand(execute: InsertItem, canExecute: _ => IsValidData);
        SelectPlayerCommand = new RelayCommand(execute: _ =>
        {
            var win = new SelectItemWindow(items: DbContext.Players.Where(predicate: p => DbContext.TeamRosters.All(tr => tr.PlayerId != p.Id)).ToArray(),
                defaultItem: DbContext.Players.FirstOrDefault(predicate: p => p.Id == PlayerId));
            win.ShowDialog();
            if (win.DialogResult != true) return;

            PlayerId = (win.ReturnItem as Player)?.Id ?? default(int);
        });
        SelectTeamCommand = new RelayCommand(execute: _ =>
        {
            var win = new SelectItemWindow(items: DbContext.Teams.ToArray(),
                defaultItem: DbContext.Teams.FirstOrDefault(predicate: t => t.Id == TeamId));
            win.ShowDialog();
            if (win.DialogResult != true) return;
            TeamId = (win.ReturnItem as Team)?.Id ?? default(int);
        });
    }

    public RelayCommand EditCommand { get; set; }
    public RelayCommand SelectPlayerCommand { get; set; }
    public RelayCommand SelectTeamCommand { get; set; }
    protected DbTournamentContext DbContext { get; }
    public TeamRoster? Item { get; set; }

    [AlsoNotifyFor(property: "PlayerName")] public int PlayerId { get; set; }
    [AlsoNotifyFor(property: "TeamName")] public int TeamId { get; set; }

    public string PlayerName => DbContext.Players.FirstOrDefault(predicate: p => p.Id == PlayerId)?.Name ?? string.Empty;
    public string TeamName => DbContext.Teams.FirstOrDefault(predicate: t => t.Id == TeamId)?.Name ?? string.Empty;
    public string PlayerPosition { get; set; }

    protected bool IsValidData
    {
        get
        {
            if (DbContext.Players.Find(keyValues: PlayerId) == null || DbContext.Teams.Find(keyValues: TeamId) == null)
                return false;
            // Additional validation rules can be added here
            if (string.IsNullOrEmpty(value: PlayerPosition))
                return false;
            return true;
        }
    }

    public void SetTeamRoster(TeamRoster? item)
    {
        if (item == null)
            return;
        Item = item;
        PlayerId = item.PlayerId ?? default(int);
        TeamId = item.TeamId ?? default(int);
        PlayerPosition = item.PlayerPosition;

        EditCommand = new RelayCommand(execute: EditItem, canExecute: _ => IsValidData);
    }

    public void SetTeam(Team? team)
    {
        if (team == null)
            return;
        TeamId = team.Id;

        EditCommand = new RelayCommand(execute: InsertItem, canExecute: _ => IsValidData);
    }
    protected void InsertItem(object obj)
    {
        DbContext.InsertTeamRoster(playerId: PlayerId, teamId: TeamId, playerPosition: PlayerPosition);

        Close(window: obj as EditTeamRosterWindow);
    }

    protected void EditItem(object obj)
    {
        DbContext.EditTeamRoster(rosterId: Item.Id, playerId: PlayerId, teamId: TeamId, playerPosition: PlayerPosition);

        Close(window: obj as EditTeamRosterWindow);
    }

    protected void Close(Window? window)
    {
        window?.Close();
    }
}