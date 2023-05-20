using System.Windows;
using System.Windows.Data;
using TournamentManagement.Models.Classes;

namespace TournamentManagement.Views.TableWindows;

/// <summary>
///     Interaction logic for TeamRosterWindow.xaml
/// </summary>
public partial class TeamRosterWindow : Window
{
    public TeamRosterWindow(Team? team = null)
    {
        FilterTeam = team;
        InitializeComponent();
    }

    public Team? FilterTeam { get; set; }

    private void ItemsViewSource_OnFilter(object sender, FilterEventArgs e)
    {
        if (FilterTeam == null) e.Accepted = true;
        else if (e.Item is TeamRoster teamRoster) e.Accepted = teamRoster.TeamId == FilterTeam?.Id;
    }
}