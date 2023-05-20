using TournamentManagement.Models.Classes;
using TournamentManagement.Views.EditWindows;
using TournamentManagement.Views.TableWindows;

namespace TournamentManagement.ViewModels.TableViewModels;

public class TournamentViewModel : TableViewModel<Tournament>
{
    public TournamentViewModel() : base(MainViewModel.Tournaments,
        new RelayCommand(_ => { new EditTournamentWindow().ShowDialog(); }),
        new RelayCommand(_ => { MainViewModel.DbTournamentContext.DeleteTournament(SelectedItem.Id); },
            _ => SelectedItem != null),
        new RelayCommand(_ => { new EditTournamentWindow(SelectedItem).ShowDialog(); },
            _ => SelectedItem != null)
    )
    {
        ViewMatchCommand = new RelayCommand(_ =>
        {
            var win = new MatchWindow(SelectedItem);
            win.ShowDialog();
        }, _ => SelectedItem != null);
    }

    public RelayCommand ViewMatchCommand { get; set; }
}