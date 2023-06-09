﻿using System;
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
                    var editTeamWindow = new EditTeamWindow();
                    editTeamWindow.ShowDialog();
                    if (editTeamWindow.DialogResult == true)
                    {
                        Items.Add(MainViewModel.Teams[^1]);
                        SelectedItem = MainViewModel.Teams[^1];
                    }

                    break;
                case { } t when t == typeof(Player):
                    var editPlayerWindow = new EditPlayerWindow();
                    editPlayerWindow.ShowDialog();
                    if (editPlayerWindow.DialogResult == true)
                    {
                        Items.Add(MainViewModel.Players[^1]);
                        SelectedItem = MainViewModel.Players[^1];
                    }

                    break;
                case { } t when t == typeof(Tournament):
                    var editTournamentWindow = new EditTournamentWindow();
                    editTournamentWindow.ShowDialog();
                    if (editTournamentWindow.DialogResult == true)
                    {
                        Items.Add(MainViewModel.Tournaments[^1]);
                        SelectedItem = MainViewModel.Tournaments[^1];
                    }

                    break;
                case { } t when t == typeof(Match):
                    var editMatchWindow = new EditMatchWindow();
                    editMatchWindow.ShowDialog();
                    if (editMatchWindow.DialogResult == true)
                    {
                        Items.Add(MainViewModel.Matches[^1]);
                        SelectedItem = MainViewModel.Matches[^1];
                    }

                    break;
                case { } t when t == typeof(TeamRoster):
                    var editTeamRosterWindow = new EditTeamRosterWindow();
                    editTeamRosterWindow.ShowDialog();
                    if (editTeamRosterWindow.DialogResult == true)
                    {
                        Items.Add(MainViewModel.TeamRosters[^1]);
                        SelectedItem = MainViewModel.TeamRosters[^1];
                    }

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
    public Type? ItemType { get; set; }


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