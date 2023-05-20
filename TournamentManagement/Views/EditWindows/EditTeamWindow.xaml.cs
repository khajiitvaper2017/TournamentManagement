using System.Windows;
using TournamentManagement.ViewModels.EditViewModels;
using Team = TournamentManagement.Models.Classes.Team;

namespace TournamentManagement.Views.EditWindows
{
    /// <summary>
    /// Interaction logic for EditTeamWindow.xaml
    /// </summary>
    public partial class EditTeamWindow : Window
    {
        public EditTeamWindow(Team? team = null)
        {
            InitializeComponent();

            var viewModel = DataContext as EditTeamViewModel;
            
            viewModel?.SetItem(team);
        }
    }
}
