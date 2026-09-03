# From the Chair

A Windows desktop companion for simple movement breaks and calisthenics.

## Current foundation

- Native WinUI navigation: Home, Routines, Progress, and Settings.
- A preferred break interval, validated and saved locally in SQLite.
- System light/dark styling and keyboard-accessible standard controls.
- Separate UI, business rules, and persistence projects, with focused tests.

Routines and Progress are honest empty states. Tray integration, autostart,
notifications, workout timers, embedded video, and measurements are **not implemented
yet**. Closing the window currently exits the app. Saving a preferred interval does
not schedule reminders.

## Project notes

Start with the [documentation index](docs/README.md):

- [App overview](docs/overview.md): goals, current status, and planned features.
- [Architecture](docs/architecture.md): project boundaries and the settings flow.
- [Development](docs/development.md): Visual Studio setup and manual build/test commands.
- [Changelog](docs/CHANGELOG.md): notable changes under Unreleased.
- [Design direction](docs/plans/design-direction.md): layout and visual decisions to make next.

The stack is C# / .NET 10, WinUI 3, CommunityToolkit.Mvvm, and SQLite. Open
`FromTheChair.slnx` in Visual Studio 2026 to work on the solution.

The next priority is design and layout before adding more workout content or features.
The user handles builds, tests, and app runs; agents follow [AGENTS.md](AGENTS.md) and
commit and push completed changes.
