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
    /// Interaction logic for MatchWindow.xaml
    /// </summary>
    public partial class MatchWindow : Window
    {
        public Tournament? FilterTournament{ get; set; }
        public MatchWindow(Tournament? tournament = null)
        {
            FilterTournament = tournament;
            InitializeComponent();
        }

        private void ItemsViewSource_OnFilter(object sender, FilterEventArgs e)
        {
            if (FilterTournament == null) e.Accepted = true;
            else if (e.Item is Match match)
            {
                e.Accepted = match.TournamentId == FilterTournament.Id;
            }
        }
    }
}
