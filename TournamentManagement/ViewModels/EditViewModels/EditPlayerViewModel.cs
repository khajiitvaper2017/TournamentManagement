﻿using System;
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

        EditCommand = new RelayCommand(execute: InsertItem, canExecute: _ => IsValidData);
        CancelCommand = new RelayCommand(execute: obj => Close(window: obj as Window));
    }

    public RelayCommand EditCommand { get; set; }
    public RelayCommand CancelCommand { get; set; }
    protected DbTournamentContext DbContext { get; }
    public Player? Item { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Nickname { get; set; }
    public string Country { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.Now;

    protected bool IsValidData
    {
        get
        {
            if (string.IsNullOrEmpty(value: FirstName) || string.IsNullOrEmpty(value: LastName) ||
                string.IsNullOrEmpty(value: Nickname) || string.IsNullOrEmpty(value: Country))
                return false;
            if (DateOfBirth.Year < 1970 || DateOfBirth.Year > DateTime.Now.Year)
                return false;
            return true;
        }
    }

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

        EditCommand = new RelayCommand(execute: EditItem, canExecute: _ => IsValidData);
    }

    protected void InsertItem(object obj)
    {
        DbContext.InsertPlayer(firstName: FirstName, lastName: LastName, nickname: Nickname, country: Country, dateOfBirth: DateOfBirth);

        Close(window: obj as EditPlayerWindow);
    }

    protected void EditItem(object obj)
    {
        DbContext.EditPlayer(playerId: Item.Id, firstName: FirstName, lastName: LastName, nickname: Nickname, country: Country, dateOfBirth: DateOfBirth);

        Close(window: obj as EditPlayerWindow);
    }

    protected void Close(Window? window)
    {
        window.DialogResult = true;
        window?.Close();
    }
}