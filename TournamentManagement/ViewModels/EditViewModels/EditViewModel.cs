using System.ComponentModel;
using System.Windows;
using TournamentManagement.Models.DbContext;

namespace TournamentManagement.ViewModels.EditViewModels;

public abstract partial class EditViewModel<T> : INotifyPropertyChanged where T : class, new()
{
    protected EditViewModel()
    {
        DbContext = MainViewModel.DbTournamentContext;

        EditCommand = new RelayCommand(InsertItem, _ => IsValidData);
    }

    public RelayCommand EditCommand { get; set; }
    protected DbTournamentContext DbContext { get; }
    public T? Item { get; set; }

    protected abstract bool IsValidData { get; }

    public abstract void SetItem(T? item);

    protected abstract void InsertItem(object obj);

    protected abstract void EditItem(object obj);

    protected void Close(Window? window)
    {
        window.DialogResult = true;
        window?.Close();
    }
}