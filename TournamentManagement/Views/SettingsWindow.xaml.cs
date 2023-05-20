using System.Windows;
using System.Windows.Controls;
using TournamentManagement.Properties;
using TournamentManagement.ViewModels;

namespace TournamentManagement.Views;

/// <summary>
///     Interaction logic for SettingsWindow.xaml
/// </summary>
public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;

        if (button?.IsCancel == true)
        {
            DialogResult = false;
            Close();
        }
        else
        {
            Settings.Default.ConnectionString = TextBox.Text;
            MainViewModel.LoadDatabase(Settings.Default.ConnectionString);

            if (MainViewModel.IsConnected == false)
            {
                MessageBox.Show("Failed to connect to database. Changes were reverted", "Error", MessageBoxButton.OK);

                Settings.Default.Reload();
                MainViewModel.LoadDatabase(Settings.Default.ConnectionString);
            }
            else
            {
                Settings.Default.Save();
                DialogResult = true;
                Close();
            }
        }
    }
}