using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TournamentManagement.ViewModels.TableViewModels;

public partial class TableViewModel<T> : INotifyPropertyChanged where T : class
{
    public TableViewModel(ObservableCollection<T> items, RelayCommand addCommand, RelayCommand deleteCommand,
        RelayCommand editCommand)
    {
        Items = items;
        AddCommand = addCommand;
        DeleteCommand = deleteCommand;
        EditCommand = editCommand;
    }

    public static ObservableCollection<T> Items { get; set; }
    public static T? SelectedItem { get; set; }
    public RelayCommand AddCommand { get; set; }
    public RelayCommand DeleteCommand { get; set; }
    public RelayCommand EditCommand { get; set; }
}