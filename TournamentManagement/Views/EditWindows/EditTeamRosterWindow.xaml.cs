﻿using System.Windows;
using TournamentManagement.Models.Classes;
using TournamentManagement.ViewModels.EditViewModels;

namespace TournamentManagement.Views.EditWindows;

/// <summary>
///     Interaction logic for EditTeamRosterWindow.xaml
/// </summary>
public partial class EditTeamRosterWindow : Window
{
    public EditTeamRosterWindow(TeamRoster teamRoster)
    {
        InitializeComponent();

        var editTeamRosterViewModel = DataContext as EditTeamRosterViewModel;
        editTeamRosterViewModel?.SetTeamRoster(teamRoster);

        Title = "Change Role of Player";
    }

    public EditTeamRosterWindow(Team team)
    {
        InitializeComponent();
        var editTeamRosterViewModel = DataContext as EditTeamRosterViewModel;
        editTeamRosterViewModel?.SetTeam(team);
        TeamSelector.Visibility = Visibility.Collapsed;
        Title = "Add Player to Team";
    }

    public EditTeamRosterWindow()
    {
        InitializeComponent();
        Title = "Assign Player to Team";
    }
}