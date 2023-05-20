using TournamentManagement.Models.Classes;
using TournamentManagement.Views.EditWindows;
using TournamentManagement.Views.TableWindows;

namespace TournamentManagement.ViewModels.TableViewModels;

public class TeamViewModel : TableViewModel<Team>
{
    public TeamViewModel() : base(items: MainViewModel.Teams,
        addCommand: new RelayCommand(execute: _ => { new EditTeamWindow().ShowDialog(); }),
        deleteCommand: new RelayCommand(execute: _ => { MainViewModel.DbTournamentContext.DeleteTeam(teamId: SelectedItem.Id); },
            canExecute: _ => SelectedItem != null),
        editCommand: new RelayCommand(execute: _ => { new EditTeamWindow(team: SelectedItem).ShowDialog(); },
            canExecute: _ => SelectedItem != null)
    )
    {
        ViewTeamRosterCommand = new RelayCommand(execute: _ =>
        {
            var win = new TeamRosterWindow(team: SelectedItem);
            win.ShowDialog();
        }, canExecute: _ => SelectedItem != null);
    }

    public RelayCommand ViewTeamRosterCommand { get; set; }
}