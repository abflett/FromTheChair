using FromTheChair.Core.Settings;
using FromTheChair.Infrastructure.Settings;
using Microsoft.Data.Sqlite;

namespace FromTheChair.Tests.Settings;

public sealed class SqlitePreferencesStoreTests : IDisposable
{
    private readonly string _directory = Path.Combine(Path.GetTempPath(), "FromTheChair.Tests", Guid.NewGuid().ToString("N"));
    private string DatabasePath => Path.Combine(_directory, "preferences.db");
    private SqlitePreferencesStore CreateStore() => new(DatabasePath);

    [Fact]
    public async Task FirstRunCreatesStorageAndReturnsDefaults()
    {
        Assert.Equal(AppPreferences.Default, await CreateStore().LoadAsync());
        Assert.True(File.Exists(DatabasePath));
    }

    [Fact]
    public async Task SavedChangesSurviveReopeningTheStore()
    {
        await CreateStore().SaveAsync(new AppPreferences(30));
        Assert.Equal(30, (await CreateStore().LoadAsync()).BreakIntervalMinutes);
        await CreateStore().SaveAsync(new AppPreferences(90));
        Assert.Equal(90, (await CreateStore().LoadAsync()).BreakIntervalMinutes);
    }

    [Fact]
    public async Task NewerDatabaseIsRejectedWithoutOverwritingItsDataOrVersion()
    {
        await CreateStore().SaveAsync(new AppPreferences(45));
        using var connection = new SqliteConnection(new SqliteConnectionStringBuilder
        {
            DataSource = DatabasePath,
            Pooling = false
        }.ToString());
        await connection.OpenAsync();
        using var command = connection.CreateCommand();
        command.CommandText = "PRAGMA user_version = 999;";
        await command.ExecuteNonQueryAsync();

        await Assert.ThrowsAsync<PreferencesStoreException>(() => CreateStore().LoadAsync());
        await Assert.ThrowsAsync<PreferencesStoreException>(() => CreateStore().SaveAsync(new AppPreferences(90)));

        command.CommandText = "PRAGMA user_version;";
        Assert.Equal(999L, await command.ExecuteScalarAsync());
        command.CommandText = "SELECT break_interval_minutes FROM app_preferences WHERE id = 1;";
        Assert.Equal(45L, await command.ExecuteScalarAsync());
    }

    [Fact]
    public async Task UnreadableDatabaseReportsFailureInsteadOfResettingPreferences()
    {
        Directory.CreateDirectory(_directory);
        const string contents = "This is not a SQLite database.";
        await File.WriteAllTextAsync(DatabasePath, contents);

        await Assert.ThrowsAsync<PreferencesStoreException>(() => CreateStore().LoadAsync());
        Assert.Equal(contents, await File.ReadAllTextAsync(DatabasePath));
    }

    public void Dispose()
    {
        // Only remove the exact files created by this test instance, never recursively.
        File.Delete(DatabasePath);
        File.Delete(DatabasePath + "-journal");
        if (Directory.Exists(_directory)) Directory.Delete(_directory);
    }
}
