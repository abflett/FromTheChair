using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FromTheChair.Core.Settings;

namespace FromTheChair.App.ViewModels;

public partial class PreferencesViewModel : ObservableObject
{
    private readonly IPreferencesStore _store;
    private AppPreferences _savedPreferences = AppPreferences.Default;
    private bool _loaded;

    public PreferencesViewModel(IPreferencesStore store)
    {
        _store = store;
        BreakIntervalMinutes = AppPreferences.Default.BreakIntervalMinutes;
        StatusMessage = "Loading your preferences...";
    }

    [ObservableProperty]
    public partial double BreakIntervalMinutes { get; set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial string StatusMessage { get; set; }

    public string SavedCadence => _loaded ? $"Every {_savedPreferences.BreakIntervalMinutes} minutes" : "Not set";
    public double MinimumInterval => AppPreferences.MinimumBreakIntervalMinutes;
    public double MaximumInterval => AppPreferences.MaximumBreakIntervalMinutes;

    public async Task LoadAsync()
    {
        IsBusy = true;
        try
        {
            _savedPreferences = await _store.LoadAsync();
            BreakIntervalMinutes = _savedPreferences.BreakIntervalMinutes;
            _loaded = true;
            OnPropertyChanged(nameof(SavedCadence));
            StatusMessage = "Your preferences stay on this device.";
        }
        catch (PreferencesStoreException exception)
        {
            Debug.WriteLine(exception);
            StatusMessage = "We couldn't read your preferences. Close and reopen the app to try again.";
        }
        finally { IsBusy = false; }
    }

    partial void OnBreakIntervalMinutesChanged(double value)
    {
        if (_loaded && !IsBusy) StatusMessage = "Save to keep your changes.";
    }

    private bool CanSave() => _loaded && !IsBusy;

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task SaveAsync()
    {
        if (!double.IsFinite(BreakIntervalMinutes) || BreakIntervalMinutes != Math.Truncate(BreakIntervalMinutes)
            || BreakIntervalMinutes < MinimumInterval || BreakIntervalMinutes > MaximumInterval)
        {
            StatusMessage = $"Enter a whole number from {MinimumInterval} to {MaximumInterval} minutes.";
            return;
        }

        IsBusy = true;
        try
        {
            var preferences = new AppPreferences((int)BreakIntervalMinutes);
            await _store.SaveAsync(preferences);
            _savedPreferences = preferences;
            OnPropertyChanged(nameof(SavedCadence));
            StatusMessage = "Preferences saved.";
        }
        catch (PreferencesStoreException exception)
        {
            Debug.WriteLine(exception);
            StatusMessage = "We couldn't save your preferences. Please try again.";
        }
        finally { IsBusy = false; }
    }
}
