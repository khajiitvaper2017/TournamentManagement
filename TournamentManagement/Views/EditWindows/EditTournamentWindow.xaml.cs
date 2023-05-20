using System.Windows;
using TournamentManagement.Models.Classes;
using TournamentManagement.ViewModels.EditViewModels;

namespace TournamentManagement.Views.EditWindows;

/// <summary>
///     Interaction logic for EditTournamentWindow.xaml
/// </summary>
public partial class EditTournamentWindow : Window
{
    public EditTournamentWindow(Tournament? tournament = null)
    {
        InitializeComponent();

        var viewModel = DataContext as EditTournamentViewModel;

        viewModel?.SetItem(tournament);
    }
}