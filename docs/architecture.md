# Architecture

The app is a modular monolith using pragmatic Clean Architecture boundaries and
MVVM. There is one desktop process, with separate projects to keep Windows UI,
business rules, and storage understandable.

## Project boundaries

| Project | Responsibility | Project dependencies |
| --- | --- | --- |
| `FromTheChair.App` | WinUI views, view models, navigation, Windows integration, startup composition | Core, Infrastructure |
| `FromTheChair.Core` | Platform-independent models, validation, and interfaces | None |
| `FromTheChair.Infrastructure` | SQLite persistence implementations | Core |
| `FromTheChair.Tests` | Focused business-rule and real SQLite tests | Core, Infrastructure |

The UI uses CommunityToolkit.Mvvm. Core has no WinUI or database dependency.
The project files and `global.json` are the source of truth for dependency and SDK
versions.

Use ordinary methods and constructor injection. A generic repository, mediator,
event bus, or separate project for every feature is unnecessary at this stage.
Group code by feature within each project as features arrive.

## Current settings flow

1. `App.OnLaunched` constructs `SqlitePreferencesStore` and `PreferencesViewModel`,
   then creates the main window. This is the composition root.
2. Settings binds editable state and its save action to the view model.
3. `AppPreferences` in Core validates the supported break interval.
4. `IPreferencesStore` is the storage boundary, implemented by
   `SqlitePreferencesStore` in Infrastructure.
5. The view model exposes saved state and user-facing load/save feedback.

Code-behind owns presentation and navigation. Validation belongs in Core; persistence
details belong in Infrastructure. The saved interval is a preference, not an active
schedule.

## Persistence

The current database is:

```text
%LOCALAPPDATA%\FromTheChair\preferences.db
```

Packaged and unpackaged development runs use the same location for the same Windows
user. Tests use isolated temporary databases.

SQLite creates schema version 1 transactionally and uses parameterized statements.
An unsupported newer schema or unreadable database produces an error instead of a
silent reset. Connections close after each operation; pooling is disabled.

Preserve existing data when the schema evolves. Add explicit migrations only when
a feature needs them.

## Future boundaries

These are guidance for later work, not services already implemented.

- **Application lifetime:** separate a visible window from a process that can
  remain available in the tray. Make exit and minimize behavior deliberate.
- **Reminders:** keep scheduling decisions separate from Windows notification
  delivery, permissions, and activation. Avoid continuous polling.
- **Time:** introduce `TimeProvider` when timing arrives. Define sleep/resume and
  overdue-reminder behavior before treating a UI timer as the scheduler.
- **Workout sessions:** keep the activity timer and session state separate from
  video playback, so pausing one does not implicitly pause the other.
- **Media:** create playback resources when needed and release them afterward.
  Decide YouTube and offline-file behavior before choosing an embedded browser
  or additional media dependency.
- **Progress:** persist real session and measurement data before adding charts.

The current foundation has no scheduler, background polling, media player, or
network requests. The next work is [design and layout](plans/design-direction.md).
