using System;
using FromTheChair.App.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FromTheChair.App.Views;

public sealed partial class HomePage : Page
{
    public PreferencesViewModel ViewModel { get; }
    public event EventHandler? ConfigureRequested;

    public HomePage(PreferencesViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }

    private void Configure_Click(object sender, RoutedEventArgs e) => ConfigureRequested?.Invoke(this, EventArgs.Empty);
}
