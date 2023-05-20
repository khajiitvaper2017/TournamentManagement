using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TournamentManagement.Models.Classes;

namespace TournamentManagement.Views.TableWindows
{
    /// <summary>
    /// Interaction logic for TeamRosterWindow.xaml
    /// </summary>
    public partial class TeamRosterWindow : Window
    {
        public Team? FilterTeam { get; set; }
        public TeamRosterWindow(Team? team = null)
        {
            FilterTeam = team;
            InitializeComponent();
        }

        private void ItemsViewSource_OnFilter(object sender, FilterEventArgs e)
        {
            if (FilterTeam == null) e.Accepted = true;
            else if (e.Item is TeamRoster teamRoster)
            {
                e.Accepted = teamRoster.TeamId == FilterTeam?.Id;
            }
        }
    }
}
