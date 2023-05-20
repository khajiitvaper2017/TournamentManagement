using TournamentManagement.Models.Classes;
using TournamentManagement.Views.EditWindows;

namespace TournamentManagement.ViewModels.TableViewModels;

public class TeamRosterViewModel : TableViewModel<TeamRoster>
{
    public TeamRosterViewModel() : base(MainViewModel.TeamRosters,
        new RelayCommand(_ => { new EditTeamRosterWindow().ShowDialog(); }),
        new RelayCommand(_ => { MainViewModel.DbTournamentContext.DeleteTeamRoster(SelectedItem.Id); },
            _ => SelectedItem != null),
        new RelayCommand(_ => { new EditTeamRosterWindow(SelectedItem).ShowDialog(); },
            _ => SelectedItem != null)
    )
    {
    }
}