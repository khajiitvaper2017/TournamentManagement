using TournamentManagement.Models.Classes;
using TournamentManagement.Views.EditWindows;
using TournamentManagement.Views.TableWindows;

namespace TournamentManagement.ViewModels.TableViewModels;

public class TeamViewModel : TableViewModel<Team>
{
    public TeamViewModel() : base(MainViewModel.Teams,
        new RelayCommand(_ => { new EditTeamWindow().ShowDialog(); }),
        new RelayCommand(_ => { MainViewModel.DbTournamentContext.DeleteTeam(SelectedItem.Id); },
            _ => SelectedItem != null),
        new RelayCommand(_ => { new EditTeamWindow(SelectedItem).ShowDialog(); },
            _ => SelectedItem != null)
    )
    {
        ViewTeamRosterCommand = new RelayCommand(_ =>
        {
            var win = new TeamRosterWindow(SelectedItem);
            win.ShowDialog();
        }, _ => SelectedItem != null);
    }

    public RelayCommand ViewTeamRosterCommand { get; set; }
}