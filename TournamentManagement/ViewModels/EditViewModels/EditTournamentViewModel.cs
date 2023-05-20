using System;
using System.ComponentModel;
using System.Windows;
using TournamentManagement.Models.Classes;
using TournamentManagement.Models.DbContext;
using TournamentManagement.Views.EditWindows;

namespace TournamentManagement.ViewModels.EditViewModels;

public partial class EditTournamentViewModel : INotifyPropertyChanged, IItem<Tournament>
{
    public EditTournamentViewModel()
    {
        DbContext = MainViewModel.DbTournamentContext;

        EditCommand = new RelayCommand(InsertItem, _ => IsValidData);
    }

    public RelayCommand EditCommand { get; set; }
    protected DbTournamentContext DbContext { get; }

    public string Name { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public string Location { get; set; }

    protected bool IsValidData
    {
        get
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Location) ||
                StartDate > EndDate)
                return false;
            return true;
        }
    }

    public Tournament? Item { get; set; }

    public void SetItem(Tournament? item)
    {
        if (item == null)
            return;
        Item = item;
        Name = item.Name;
        StartDate = item.StartDate ?? DateTime.Now;
        EndDate = item.EndDate ?? DateTime.Now;
        Location = item.Location;

        EditCommand = new RelayCommand(EditItem, _ => IsValidData);
    }

    protected void InsertItem(object obj)
    {
        DbContext.InsertTournament(Name, StartDate, EndDate, Location);

        Close(obj as EditTournamentWindow);
    }

    protected void EditItem(object obj)
    {
        DbContext.EditTournament(Item.Id, Name, StartDate, EndDate, Location);

        Close(obj as EditTournamentWindow);
    }

    protected void Close(Window? window)
    {
        window?.Close();
    }
}