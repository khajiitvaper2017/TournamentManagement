using System;
using System.Windows.Input;

namespace TournamentManagement.ViewModels;

public class RelayCommand : ICommand
{
    private readonly Predicate<object>? canExecute;
    private readonly Action<object> execute;

    public RelayCommand(Action<object> execute, Predicate<object>? canExecute = null)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public bool CanExecute(object? parameter = null)
    {
        return canExecute == null || canExecute(obj: parameter);
    }

    public void Execute(object? parameter = null)
    {
        execute(obj: parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}