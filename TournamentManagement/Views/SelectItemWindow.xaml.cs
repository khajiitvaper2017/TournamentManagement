using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TournamentManagement.Models.Interfaces;
using TournamentManagement.ViewModels;

namespace TournamentManagement.Views;

/// <summary>
///     Interaction logic for SelectItemWindow.xaml
/// </summary>
public partial class SelectItemWindow : Window
{
    public SelectItemWindow(ICollection<IDbItem> items, IDbItem? defaultItem = null)
    {
        InitializeComponent();

        if (DataContext is not SelectItemViewModel viewModel) return;

        viewModel.ItemType = items.GetType().GetElementType();
        Title = $"Select {viewModel.ItemType.Name}";

        viewModel.Items = new ObservableCollection<IDbItem>(items);
        viewModel.SelectedItem =
            defaultItem == null ? null : viewModel.Items.FirstOrDefault(i => i.Id == defaultItem.Id);
    }

    public int ItemsCount => (DataContext as SelectItemViewModel)?.Items.Count ?? 0;

    public IDbItem ReturnItem { get; set; }
}