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

        if (items.Count == 0)
        {
            MessageBox.Show(messageBoxText: "No items available to select");
            Close();
            return;
        }

        viewModel.ItemType = items.First().GetType();
        Title = $"Select {viewModel.ItemType.Name}";

        viewModel.Items = new ObservableCollection<IDbItem>(collection: items);
        viewModel.SelectedItem = defaultItem == null ? viewModel.Items.First() : viewModel.Items.FirstOrDefault(predicate: i => i.Id == defaultItem.Id);
    }

    public IDbItem ReturnItem { get; set; }
}