using System.Windows;
using System.Windows.Data;
using TournamentManagement.Models.Classes;

namespace TournamentManagement.Views.TableWindows;

/// <summary>
///     Interaction logic for MatchWindow.xaml
/// </summary>
public partial class MatchWindow : Window
{
    public MatchWindow(Tournament? tournament = null)
    {
        FilterTournament = tournament;
        InitializeComponent();
    }

    public Tournament? FilterTournament { get; set; }

    private void ItemsViewSource_OnFilter(object sender, FilterEventArgs e)
    {
        if (FilterTournament == null) e.Accepted = true;
        else if (e.Item is Match match) e.Accepted = match.TournamentId == FilterTournament.Id;
    }
}