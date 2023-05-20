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

namespace TournamentManagement.Views.EditWindows
{
    /// <summary>
    /// Interaction logic for EditTournamentWindow.xaml
    /// </summary>
    public partial class EditTournamentWindow : Window
    {
        public EditTournamentWindow(Tournament? tournament = null)
        {
            InitializeComponent();

            var viewModel = DataContext as ViewModels.EditViewModels.EditTournamentViewModel;

            viewModel?.SetItem(item: tournament);
        }
    }
}
