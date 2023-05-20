using TournamentManagement.Models.Classes;
using TournamentManagement.Views.EditWindows;
using TournamentManagement.Views.TableWindows;

namespace TournamentManagement.ViewModels.TableViewModels;

public class TournamentViewModel : TableViewModel<Tournament>
{
    public TournamentViewModel() : base(items: MainViewModel.Tournaments,
        addCommand: new RelayCommand(execute: _ => { new EditTournamentWindow().ShowDialog(); }),
        deleteCommand: new RelayCommand(execute: _ => { MainViewModel.DbTournamentContext.DeleteTournament(tournamentId: SelectedItem.Id); },
            canExecute: _ => SelectedItem != null),
        editCommand: new RelayCommand(execute: _ => { new EditTournamentWindow(tournament: SelectedItem).ShowDialog(); },
            canExecute: _ => SelectedItem != null)
    )
    {
        ViewMatchCommand = new RelayCommand(execute: _ =>
        {
            var win = new MatchWindow(tournament: SelectedItem);
            win.ShowDialog();
        }, canExecute: _ => SelectedItem != null);
    }

    public RelayCommand ViewMatchCommand { get; set; }
}