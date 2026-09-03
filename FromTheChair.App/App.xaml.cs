using System;
using System.IO;
using FromTheChair.App.ViewModels;
using FromTheChair.Infrastructure.Settings;
using Microsoft.UI.Xaml;

namespace FromTheChair.App;

public partial class App : Application
{
    private Window? _window;
    public App() => InitializeComponent();

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        // Composition root: startup chooses the persistence implementation.
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "FromTheChair", "preferences.db");
        var preferences = new PreferencesViewModel(new SqlitePreferencesStore(path));
        _window = new MainWindow(preferences);
        _window.Activate();
    }
}
