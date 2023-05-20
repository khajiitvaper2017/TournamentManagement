using TournamentManagement.Models.Classes;
using TournamentManagement.Views.EditWindows;

namespace TournamentManagement.ViewModels.TableViewModels;

public class PlayerViewModel : TableViewModel<Player>
{
    public PlayerViewModel() : base(MainViewModel.Players,
        new RelayCommand(_ => { new EditPlayerWindow().ShowDialog(); }),
        new RelayCommand(_ => { MainViewModel.DbTournamentContext.DeletePlayer(SelectedItem.Id); },
            _ => SelectedItem != null),
        new RelayCommand(_ => { new EditPlayerWindow(SelectedItem).ShowDialog(); },
            _ => SelectedItem != null)
    )
    {
    }
}