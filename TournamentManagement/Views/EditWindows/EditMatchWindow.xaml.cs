using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TournamentManagement.ViewModels.EditViewModels;

namespace TournamentManagement.Views.EditWindows
{
    /// <summary>
    /// Interaction logic for EditMatchWindow.xaml
    /// </summary>
    public partial class EditMatchWindow : Window
    {
        public EditMatchWindow(Models.Classes.Match? match = null)
        {
            InitializeComponent();

            if (match == null) return;
            var viewModel = DataContext as EditMatchViewModel;
            viewModel?.SetItem(match);
        }
    }
}
