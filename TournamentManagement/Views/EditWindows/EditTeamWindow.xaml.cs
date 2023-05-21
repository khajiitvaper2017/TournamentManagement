using System.Windows;
using TournamentManagement.Models.Classes;
using TournamentManagement.ViewModels.EditViewModels;

namespace TournamentManagement.Views.EditWindows;

/// <summary>
///     Interaction logic for EditTeamWindow.xaml
/// </summary>
public partial class EditTeamWindow : Window
{
    public EditTeamWindow(Team? team = null)
    {
        InitializeComponent();

        var viewModel = DataContext as EditTeamViewModel;
        if(team == null) return;

        viewModel?.SetItem(team);
        Title = "Edit Team";
    }
}