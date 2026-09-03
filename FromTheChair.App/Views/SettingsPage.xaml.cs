using FromTheChair.App.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace FromTheChair.App.Views;

public sealed partial class SettingsPage : Page
{
    public PreferencesViewModel ViewModel { get; }

    public SettingsPage(PreferencesViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }
}
