using TournamentManagement.Models.Classes;
using TournamentManagement.ViewModels.EditViewModels;
using TournamentManagement.Views.EditWindows;

namespace TournamentManagement.ViewModels.TableViewModels;

public class MatchViewModel : TableViewModel<Match>
{
    public MatchViewModel() : base(items: MainViewModel.Matches,
        addCommand: new RelayCommand(execute: _ =>
        {
            var emw = new EditMatchWindow();
            var dataContext = emw.DataContext as EditMatchViewModel;
            dataContext!.TournamentId = SelectedItem.TournamentId ?? 0;
            emw.ShowDialog();
        }),
        deleteCommand: new RelayCommand(execute: _ => { MainViewModel.DbTournamentContext.DeleteMatch(matchId: SelectedItem.Id); },
            canExecute: _ => SelectedItem != null),
        editCommand: new RelayCommand(execute: _ => { new EditMatchWindow(match: SelectedItem).ShowDialog(); },
            canExecute: _ => SelectedItem != null)
    )
    {
    }
}