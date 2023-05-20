using TournamentManagement.Models.Classes;
using TournamentManagement.ViewModels.EditViewModels;
using TournamentManagement.Views.EditWindows;

namespace TournamentManagement.ViewModels.TableViewModels;

public class MatchViewModel : TableViewModel<Match>
{
    public MatchViewModel() : base(MainViewModel.Matches,
        new RelayCommand(_ =>
        {
            var emw = new EditMatchWindow();
            var dataContext = emw.DataContext as EditMatchViewModel;
            dataContext!.TournamentId = SelectedItem.TournamentId ?? 0;
            emw.ShowDialog();
        }),
        new RelayCommand(_ => { MainViewModel.DbTournamentContext.DeleteMatch(SelectedItem.Id); },
            _ => SelectedItem != null),
        new RelayCommand(_ => { new EditMatchWindow(SelectedItem).ShowDialog(); },
            _ => SelectedItem != null)
    )
    {
    }
}