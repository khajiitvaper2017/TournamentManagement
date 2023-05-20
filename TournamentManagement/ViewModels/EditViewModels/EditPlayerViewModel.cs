using System;
using System.ComponentModel;
using System.Windows;
using TournamentManagement.Models.Classes;
using TournamentManagement.Models.DbContext;
using TournamentManagement.Views.EditWindows;

namespace TournamentManagement.ViewModels.EditViewModels;

public partial class EditPlayerViewModel : INotifyPropertyChanged, IItem<Player>
{
    public EditPlayerViewModel()
    {
        DbContext = MainViewModel.DbTournamentContext;

        EditCommand = new RelayCommand(InsertItem, _ => IsValidData);
        CancelCommand = new RelayCommand(obj => Close(obj as Window));
    }

    public RelayCommand EditCommand { get; set; }
    public RelayCommand CancelCommand { get; set; }
    protected DbTournamentContext DbContext { get; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Nickname { get; set; }
    public string Country { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.Now;

    protected bool IsValidData
    {
        get
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) ||
                string.IsNullOrEmpty(Nickname) || string.IsNullOrEmpty(Country))
                return false;
            if (DateOfBirth.Year < 1970 || DateOfBirth.Year > DateTime.Now.Year)
                return false;
            return true;
        }
    }

    public Player? Item { get; set; }

    public void SetItem(Player? item)
    {
        if (item == null)
            return;
        Item = item;
        FirstName = item.FirstName;
        LastName = item.LastName;
        Nickname = item.Nickname;
        Country = item.Country;
        DateOfBirth = item.DateOfBirth ?? DateTime.Now;

        EditCommand = new RelayCommand(EditItem, _ => IsValidData);
    }

    protected void InsertItem(object obj)
    {
        DbContext.InsertPlayer(FirstName, LastName, Nickname, Country, DateOfBirth);

        Close(obj as EditPlayerWindow);
    }

    protected void EditItem(object obj)
    {
        DbContext.EditPlayer(Item.Id, FirstName, LastName, Nickname, Country, DateOfBirth);

        Close(obj as EditPlayerWindow);
    }

    protected void Close(Window? window)
    {
        window.DialogResult = true;
        window?.Close();
    }
}