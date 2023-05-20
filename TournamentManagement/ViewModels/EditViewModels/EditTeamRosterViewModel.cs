using System.ComponentModel;
using System.Linq;
using System.Windows;
using PropertyChanged;
using TournamentManagement.Models.Classes;
using TournamentManagement.Models.DbContext;
using TournamentManagement.Models.Interfaces;
using TournamentManagement.Views;
using TournamentManagement.Views.EditWindows;

namespace TournamentManagement.ViewModels.EditViewModels;

public partial class EditTeamRosterViewModel : INotifyPropertyChanged, IItem<TeamRoster>
{
    public EditTeamRosterViewModel()
    {
        DbContext = MainViewModel.DbTournamentContext;

        EditCommand = new RelayCommand(InsertItem, _ => IsValidData);
        SelectPlayerCommand = new RelayCommand(obj =>
        {
            var win = new SelectItemWindow(
                    DbContext.Players.Where(p => DbContext.TeamRosters.All(tr => tr.PlayerId != p.Id)).ToArray(),
                    DbContext.Players.FirstOrDefault(p => p.Id == PlayerId))
                { Owner = obj as Window };

            win.ShowDialog();
            if (win.DialogResult != true) return;

            PlayerId = (win.ReturnItem as Player)?.Id ?? default(int);
        });
        SelectTeamCommand = new RelayCommand(obj =>
        {
            var win = new SelectItemWindow(DbContext.Teams.ToArray(),
                    DbContext.Teams.FirstOrDefault(t => t.Id == TeamId))
                { Owner = obj as Window };
            win.ShowDialog();
            if (win.DialogResult != true) return;
            TeamId = (win.ReturnItem as Team)?.Id ?? default(int);
        });
    }

    public RelayCommand EditCommand { get; set; }
    public RelayCommand SelectPlayerCommand { get; set; }
    public RelayCommand SelectTeamCommand { get; set; }
    protected DbTournamentContext DbContext { get; }

    [AlsoNotifyFor("PlayerName")] public int PlayerId { get; set; }
    [AlsoNotifyFor("TeamName")] public int TeamId { get; set; }

    public string PlayerName => DbContext.Players.FirstOrDefault(p => p.Id == PlayerId)?.Name ?? string.Empty;
    public string TeamName => DbContext.Teams.FirstOrDefault(t => t.Id == TeamId)?.Name ?? string.Empty;
    public string PlayerPosition { get; set; }

    protected bool IsValidData
    {
        get
        {
            if (DbContext.Players.Find(keyValues: PlayerId) == null || DbContext.Teams.Find(keyValues: TeamId) == null)
                return false;
            // Additional validation rules can be added here
            if (string.IsNullOrEmpty(PlayerPosition))
                return false;
            return true;
        }
    }

    public TeamRoster? Item { get; set; }

    public void SetTeamRoster(TeamRoster? item)
    {
        if (item == null)
            return;
        Item = item;
        PlayerId = item.PlayerId ?? default(int);
        TeamId = item.TeamId ?? default(int);
        PlayerPosition = item.PlayerPosition;

        EditCommand = new RelayCommand(EditItem, _ => IsValidData);
    }

    public void SetTeam(Team? team)
    {
        if (team == null)
            return;
        TeamId = team.Id;

        EditCommand = new RelayCommand(InsertItem, _ => IsValidData);
    }

    protected void InsertItem(object obj)
    {
        DbContext.InsertTeamRoster(PlayerId, TeamId, PlayerPosition);

        Close(obj as EditTeamRosterWindow);
    }

    protected void EditItem(object obj)
    {
        DbContext.EditTeamRoster(Item.Id, PlayerId, TeamId, PlayerPosition);

        Close(obj as EditTeamRosterWindow);
    }

    protected void Close(Window? window)
    {
        window?.Close();
    }
}

public interface IItem<T> where T : class, IDbItem
{
    public T? Item { get; set; }
}