using FromTheChair.Core.Settings;

namespace FromTheChair.Tests.Settings;

public sealed class AppPreferencesTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(14)]
    [InlineData(241)]
    public void RejectsIntervalsOutsideTheSupportedRange(int minutes)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new AppPreferences(minutes));
    }

    [Theory]
    [InlineData(15)]
    [InlineData(240)]
    public void AcceptsTheBoundaryIntervals(int minutes)
    {
        Assert.Equal(minutes, new AppPreferences(minutes).BreakIntervalMinutes);
    }
}
