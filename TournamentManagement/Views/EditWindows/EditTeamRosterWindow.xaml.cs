using System.Windows;
using TournamentManagement.Models.Classes;
using TournamentManagement.ViewModels.EditViewModels;

namespace TournamentManagement.Views.EditWindows;

/// <summary>
///     Interaction logic for EditTeamRosterWindow.xaml
/// </summary>
public partial class EditTeamRosterWindow : Window
{
    public EditTeamRosterWindow(TeamRoster? teamRoster = null)
    {
        InitializeComponent();

        var editTeamRosterViewModel = DataContext as EditTeamRosterViewModel;
        editTeamRosterViewModel?.SetItem(teamRoster);
    }
}