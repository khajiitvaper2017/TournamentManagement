using System;
using System.ComponentModel;
using System.Windows;
using TournamentManagement.Models.Classes;
using TournamentManagement.Models.DbContext;
using TournamentManagement.Views.EditWindows;

namespace TournamentManagement.ViewModels.EditViewModels;

public partial class EditTeamViewModel : INotifyPropertyChanged
{
    public EditTeamViewModel()
    {
        DbContext = MainViewModel.DbTournamentContext;

        EditCommand = new RelayCommand(execute: InsertItem, canExecute: _ => IsValidData);
    }

    public RelayCommand EditCommand { get; set; }
    protected DbTournamentContext DbContext { get; }
    public Team? Item { get; set; }

    public string TeamName { get; set; }
    public string Country { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public int Wins { get; set; }
    public int Losses { get; set; }

    protected bool IsValidData
    {
        get
        {
            if (string.IsNullOrEmpty(value: TeamName) || string.IsNullOrEmpty(value: Country))
                return false;
            if (DateCreated.Year < 1970 || DateCreated.Year > DateTime.Now.Year)
                return false;
            if (Wins < 0 || Losses < 0)
                return false;
            if (DateCreated.Year < 1970 && DateCreated.Year > DateTime.Now.Year)
                return false;
            return true;
        }
    }

    public void SetItem(Team? item)
    {
        if (item == null)
            return;
        Item = item;
        TeamName = item.Name;
        Country = item.Country;
        DateCreated = item.DateCreated ?? DateTime.Now;
        Wins = item.Wins ?? 0;
        Losses = item.Losses ?? 0;

        EditCommand = new RelayCommand(execute: EditItem, canExecute: _ => IsValidData);
    }

    protected void InsertItem(object obj)
    {
        DbContext.InsertTeam(teamName: TeamName, country: Country, dateCreated: DateCreated, wins: Wins, losses: Losses);

        Close(window: obj as EditTeamWindow);
    }

    protected void EditItem(object obj)
    {
        DbContext.EditTeam(teamId: Item.Id, teamName: TeamName, country: Country, dateCreated: DateCreated, wins: Wins, losses: Losses);

        Close(window: obj as EditTeamWindow);
    }

    protected void Close(Window? window)
    {
        window?.Close();
    }
}