using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using TournamentManagement.Models.Classes;
using TournamentManagement.Models.Interfaces;
using TournamentManagement.Views;
using TournamentManagement.Views.EditWindows;

namespace TournamentManagement.ViewModels;

public partial class SelectItemViewModel : INotifyPropertyChanged
{
    public SelectItemViewModel()
    {
        OkCommand = new RelayCommand(Ok, _ => SelectedItem != null);
        CancelCommand = new RelayCommand(Cancel);
        AddCommand = new RelayCommand(_ =>
        {
            switch (ItemType)
            {
                case { } t when t == typeof(Team):
                    new EditTeamWindow().ShowDialog();
                    break;
                case { } t when t == typeof(Player):
                    new EditPlayerWindow().ShowDialog();
                    break;
                case { } t when t == typeof(Tournament):
                    new EditTournamentWindow().ShowDialog();
                    break;
                case { } t when t == typeof(Match):
                    new EditMatchWindow().ShowDialog();
                    break;
                case { } t when t == typeof(TeamRoster):
                    new EditTeamRosterWindow().ShowDialog();
                    break;
            }
        }, _ => ItemType != null);
    }

    public string Title { get; set; }
    public ObservableCollection<IDbItem> Items { get; set; }
    public IDbItem? SelectedItem { get; set; }
    public RelayCommand OkCommand { get; set; }
    public RelayCommand CancelCommand { get; set; }
    public RelayCommand AddCommand { get; set; }
    public Type ItemType { get; set; }


    public void Ok(object parameter)
    {
        (parameter as SelectItemWindow).ReturnItem = SelectedItem;
        Close(parameter as Window, true);
    }

    public void Cancel(object parameter)
    {
        Close(parameter as Window, false);
    }

    public void Close(Window? window, bool result)
    {
        if (window == null) return;
        window.DialogResult = result;
        window.Close();
    }
}