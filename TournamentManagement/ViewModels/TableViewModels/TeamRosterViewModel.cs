using TournamentManagement.Models.Classes;
using TournamentManagement.Views.EditWindows;

namespace TournamentManagement.ViewModels.TableViewModels;

public class TeamRosterViewModel : TableViewModel<TeamRoster>
{
    public TeamRosterViewModel() : base(items: MainViewModel.TeamRosters,
        addCommand: new RelayCommand(execute: _ => { new EditTeamRosterWindow(team: SelectedItem.Team).ShowDialog(); }),
        deleteCommand: new RelayCommand(execute: _ => { MainViewModel.DbTournamentContext.DeleteTeamRoster(rosterId: SelectedItem.Id); },
            canExecute: _ => SelectedItem != null),
        editCommand: new RelayCommand(execute: _ => { new EditTeamRosterWindow(teamRoster: SelectedItem).ShowDialog(); },
            canExecute: _ => SelectedItem != null)
    )
    {
    }
}