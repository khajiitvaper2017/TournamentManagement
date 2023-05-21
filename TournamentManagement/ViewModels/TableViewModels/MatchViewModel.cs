using System.Linq;
using TournamentManagement.Models.Classes;
using TournamentManagement.ViewModels.EditViewModels;
using TournamentManagement.Views.EditWindows;
using TournamentManagement.Views.TableWindows;

namespace TournamentManagement.ViewModels.TableViewModels;

public class MatchViewModel : TableViewModel<Match>
{
    public MatchViewModel() : base(MainViewModel.Matches,
        new RelayCommand(_ =>
        {
            var emw = new EditMatchWindow();
            var dataContext = emw.DataContext as EditMatchViewModel;
            dataContext!.TournamentId = Items.FirstOrDefault()?.TournamentId ?? 0;
            emw.ShowDialog();
        }),
        new RelayCommand(_ => { MainViewModel.DbTournamentContext.DeleteMatch(SelectedItem.Id); },
            _ => SelectedItem != null),
        new RelayCommand(_ => { new EditMatchWindow(SelectedItem).ShowDialog(); },
            _ => SelectedItem != null)
    )
    {
        ViewTeam1RosterCommand = new RelayCommand(_ =>
        {
            var team = MainViewModel.DbTournamentContext.Teams.Find(SelectedItem.Team1Id);
            var teamRoster = new TeamRosterWindow(team);
            teamRoster.ShowDialog();
        }, _ => SelectedItem != null);

        ViewTeam2RosterCommand = new RelayCommand(_ =>
        {
            var team = MainViewModel.DbTournamentContext.Teams.Find(SelectedItem.Team2Id);
            var teamRoster = new TeamRosterWindow(team);
            teamRoster.ShowDialog();
        }, _ => SelectedItem != null);
    }

    public RelayCommand ViewTeam1RosterCommand { get; set; }
    public RelayCommand ViewTeam2RosterCommand { get; set; }
}