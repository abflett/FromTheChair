using FromTheChair.Core.Settings;
using Microsoft.Data.Sqlite;

namespace FromTheChair.Infrastructure.Settings;

/// <summary>Owns the first database schema. Connections live only for one operation.</summary>
public sealed class SqlitePreferencesStore : IPreferencesStore
{
    private const long CurrentSchemaVersion = 1;
    private readonly string _databasePath;

    public SqlitePreferencesStore(string databasePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(databasePath);
        _databasePath = Path.GetFullPath(databasePath);
    }

    public async Task<AppPreferences> LoadAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            using var connection = await OpenConnectionAsync(cancellationToken);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT break_interval_minutes FROM app_preferences WHERE id = 1;";
            var value = await command.ExecuteScalarAsync(cancellationToken);
            return value is null ? AppPreferences.Default : new AppPreferences(checked((int)(long)value));
        }
        catch (Exception exception) when (IsStorageFailure(exception))
        {
            throw new PreferencesStoreException("Could not load your saved preferences.", exception);
        }
    }

    public async Task SaveAsync(AppPreferences preferences, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(preferences);
        try
        {
            using var connection = await OpenConnectionAsync(cancellationToken);
            using var command = connection.CreateCommand();
            command.CommandText = """
                INSERT INTO app_preferences (id, break_interval_minutes) VALUES (1, $interval)
                ON CONFLICT(id) DO UPDATE SET break_interval_minutes = excluded.break_interval_minutes;
                """;
            command.Parameters.AddWithValue("$interval", preferences.BreakIntervalMinutes);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
        catch (Exception exception) when (IsStorageFailure(exception))
        {
            throw new PreferencesStoreException("Could not save your preferences.", exception);
        }
    }

    private async Task<SqliteConnection> OpenConnectionAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Directory.CreateDirectory(Path.GetDirectoryName(_databasePath)!);
        var connection = new SqliteConnection(new SqliteConnectionStringBuilder
        {
            DataSource = _databasePath,
            Pooling = false,
            DefaultTimeout = 3
        }.ToString());

        try
        {
            await connection.OpenAsync(cancellationToken);
            using var versionCommand = connection.CreateCommand();
            versionCommand.CommandText = "PRAGMA user_version;";
            var version = (long)(await versionCommand.ExecuteScalarAsync(cancellationToken))!;
            if (version > CurrentSchemaVersion)
            {
                throw new PreferencesStoreException("These preferences were saved by a newer version of the app.");
            }

            if (version == 0)
            {
                // Keep schema creation and its version marker atomic.
                using var transaction = connection.BeginTransaction();
                using var migration = connection.CreateCommand();
                migration.Transaction = transaction;
                migration.CommandText = """
                    CREATE TABLE IF NOT EXISTS app_preferences (
                        id INTEGER PRIMARY KEY CHECK (id = 1),
                        break_interval_minutes INTEGER NOT NULL CHECK (break_interval_minutes BETWEEN 15 AND 240)
                    );
                    PRAGMA user_version = 1;
                    """;
                await migration.ExecuteNonQueryAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }

            return connection;
        }
        catch
        {
            connection.Dispose();
            throw;
        }
    }

    private static bool IsStorageFailure(Exception exception) =>
        exception is SqliteException or IOException or UnauthorizedAccessException
            or ArgumentException or OverflowException or InvalidCastException;
}
