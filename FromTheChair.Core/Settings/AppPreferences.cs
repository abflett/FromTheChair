namespace FromTheChair.Core.Settings;

/// <summary>A preferred cadence, independent of whether reminders are running.</summary>
public sealed record AppPreferences
{
    public const int MinimumBreakIntervalMinutes = 15;
    public const int MaximumBreakIntervalMinutes = 240;
    public static AppPreferences Default { get; } = new(60);
    public int BreakIntervalMinutes { get; }

    public AppPreferences(int breakIntervalMinutes)
    {
        if (breakIntervalMinutes is < MinimumBreakIntervalMinutes or > MaximumBreakIntervalMinutes)
        {
            throw new ArgumentOutOfRangeException(nameof(breakIntervalMinutes),
                $"Choose an interval between {MinimumBreakIntervalMinutes} and {MaximumBreakIntervalMinutes} minutes.");
        }

        BreakIntervalMinutes = breakIntervalMinutes;
    }
}
