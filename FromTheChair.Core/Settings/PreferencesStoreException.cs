namespace FromTheChair.Core.Settings;

/// <summary>A persistence failure that callers can handle without knowing the database provider.</summary>
public sealed class PreferencesStoreException(string message, Exception? innerException = null)
    : Exception(message, innerException);
