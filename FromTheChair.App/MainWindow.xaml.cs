using FromTheChair.App.ViewModels;
using FromTheChair.App.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Graphics;

namespace FromTheChair.App;

public sealed partial class MainWindow : Window
{
    private readonly PreferencesViewModel _preferences;
    private bool _initialized;

    public MainWindow(PreferencesViewModel preferences)
    {
        _preferences = preferences;
        InitializeComponent();
        AppWindow.Resize(new SizeInt32(1080, 760));
    }

    private async void Navigation_Loaded(object sender, RoutedEventArgs e)
    {
        if (_initialized) return;
        _initialized = true;
        Navigation.SelectedItem = Navigation.MenuItems[0];
        await _preferences.LoadAsync();
    }

    private void Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            PageContent.Content = new SettingsPage(_preferences);
            return;
        }

        PageContent.Content = ((args.SelectedItem as NavigationViewItem)?.Tag as string) switch
        {
            "routines" => new RoutinesPage(),
            "progress" => new ProgressPage(),
            _ => CreateHomePage()
        };
    }

    private HomePage CreateHomePage()
    {
        var page = new HomePage(_preferences);
        page.ConfigureRequested += (_, _) => Navigation.SelectedItem = Navigation.SettingsItem;
        return page;
    }
}
