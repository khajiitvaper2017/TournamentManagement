using System.Windows;
using TournamentManagement.Models.Classes;
using TournamentManagement.ViewModels.EditViewModels;

namespace TournamentManagement.Views.EditWindows;

/// <summary>
///     Interaction logic for EditPlayerWindow.xaml
/// </summary>
public partial class EditPlayerWindow : Window
{
    public EditPlayerWindow(Player? player = null)
    {
        InitializeComponent();

        var viewModel = DataContext as EditPlayerViewModel;

        viewModel?.SetItem(player);
    }
}