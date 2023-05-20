using TournamentManagement.Models.Classes;
using TournamentManagement.Views.EditWindows;

namespace TournamentManagement.ViewModels.TableViewModels;

public class PlayerViewModel : TableViewModel<Player>
{
    public PlayerViewModel() : base(items: MainViewModel.Players,
        addCommand: new RelayCommand(execute: _ => { new EditPlayerWindow().ShowDialog(); }),
        deleteCommand: new RelayCommand(execute: _ => { MainViewModel.DbTournamentContext.DeletePlayer(playerId: SelectedItem.Id); },
            canExecute: _ => SelectedItem != null),
        editCommand: new RelayCommand(execute: _ => { new EditPlayerWindow(player: SelectedItem).ShowDialog(); },
            canExecute: _ => SelectedItem != null)
    )
    {
    }
}