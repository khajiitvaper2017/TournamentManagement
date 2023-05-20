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
using TournamentManagement.ViewModels.EditViewModels;

namespace TournamentManagement.Views.EditWindows
{
    /// <summary>
    /// Interaction logic for EditPlayerWindow.xaml
    /// </summary>
    public partial class EditPlayerWindow : Window
    {
        public EditPlayerWindow(Player? player = null)
        {
            InitializeComponent();

            var viewModel = DataContext as EditPlayerViewModel;

            viewModel?.SetItem(item: player);
        }
    }
}
