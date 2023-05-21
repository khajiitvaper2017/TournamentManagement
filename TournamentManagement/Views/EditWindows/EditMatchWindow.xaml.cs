using System.Windows;
using TournamentManagement.Models.Classes;
using TournamentManagement.ViewModels.EditViewModels;

namespace TournamentManagement.Views.EditWindows;

/// <summary>
///     Interaction logic for EditMatchWindow.xaml
/// </summary>
public partial class EditMatchWindow : Window
{
    public EditMatchWindow(Match? match = null)
    {
        InitializeComponent();

        if (match == null) return;
        var viewModel = DataContext as EditMatchViewModel;
        viewModel?.SetItem(match);
        if(viewModel.TournamentId != 0)
            TounamentSelector.Visibility = Visibility.Collapsed;
        Title = "Edit Match";
    }
}