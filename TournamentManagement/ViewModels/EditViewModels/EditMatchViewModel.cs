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

public partial class EditMatchViewModel : INotifyPropertyChanged
{
    public EditMatchViewModel()
    {
        DbContext = MainViewModel.DbTournamentContext;

        EditCommand = new RelayCommand(InsertItem, _ => IsValidData);
        SelectTeam1Command = new RelayCommand(_ =>
        {
            var win = new SelectItemWindow(DbContext.Teams.Where(t => t.Id != Team2Id).ToArray(),
                DbContext.Teams.FirstOrDefault(t => t.Id == Team1Id));
            win.ShowDialog();
            if (win.DialogResult != true) return;

            Team1Id = (win.ReturnItem as Team)?.Id ?? default(int);
        });
        SelectTeam2Command = new RelayCommand(_ =>
        {
            var win = new SelectItemWindow(DbContext.Teams.Where(t => t.Id != Team1Id).ToArray(),
                DbContext.Teams.FirstOrDefault(t => t.Id == Team2Id));
            win.ShowDialog();
            if (win.DialogResult != true) return;
            Team2Id = (win.ReturnItem as Team)?.Id ?? default(int);
        });
        SelectTournamentCommand = new RelayCommand(_ =>
        {
            var win = new SelectItemWindow(DbContext.Tournaments.ToArray(),
                DbContext.Tournaments.FirstOrDefault(t => t.Id == TournamentId));
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
    [AlsoNotifyFor("Team1Name")] public int Team1Id { get; set; }
    [AlsoNotifyFor("Team2Name")] public int Team2Id { get; set; }
    [AlsoNotifyFor("TournamentName")] public int TournamentId { get; set; }

    [AlsoNotifyFor("ResultOptions")]
    public string Team1Name => DbContext.Teams.FirstOrDefault(t => t.Id == Team1Id)?.Name ?? string.Empty;

    [AlsoNotifyFor("ResultOptions")]
    public string Team2Name => DbContext.Teams.FirstOrDefault(t => t.Id == Team2Id)?.Name ?? string.Empty;

    public string TournamentName =>
        DbContext.Tournaments.FirstOrDefault(t => t.Id == TournamentId)?.Name ?? string.Empty;

    public DateTime Date { get; set; } = DateTime.Now;
    public string Result { get; set; }
    public ObservableCollection<string> ResultOptions => new() { $"{Team1Name} Win", $"{Team2Name} Win" };

    protected bool IsValidData
    {
        get
        {
            if (DbContext.Teams.Find(Team1Id) == null || DbContext.Teams.Find(Team2Id) == null ||
                DbContext.Tournaments.Find(TournamentId) == null)
                return false;
            if (DbContext.Tournaments.Find(TournamentId)?.StartDate > Date ||
                DbContext.Tournaments.Find(TournamentId)?.EndDate < Date)
                return false;
            if (string.IsNullOrEmpty(Result))
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

        EditCommand = new RelayCommand(EditItem, _ => IsValidData);
    }

    protected void InsertItem(object obj)
    {
        DbContext.InsertMatch(TournamentId, Team1Id, Team2Id, Date, Result);

        Close(obj as EditMatchWindow);
    }

    protected void EditItem(object obj)
    {
        DbContext.EditMatch(Item.Id, TournamentId, Team1Id, Team2Id, Date, Result);

        Close(obj as EditMatchWindow);
    }

    protected void Close(Window? window)
    {
        window?.Close();
    }
}