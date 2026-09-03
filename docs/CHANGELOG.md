# Changelog

Notable project changes are recorded here. Work remains under **Unreleased** until
there is an actual release; dated version sections can be added then.

## Unreleased

### Added

- Native WinUI shell with Home, Routines, Progress, and Settings navigation, standard
  controls, system theme support, and a Mica backdrop.
- A preferred break interval with validation, explicit save feedback, and local
  SQLite persistence.
- Separate Core, Infrastructure, and Tests projects alongside the desktop app.
- Focused tests for preference rules and SQLite data handling.
- Repository agent instructions, a documentation index, product and architecture
  notes, manual development instructions, and a plan for the next design discussion.
- Product planning notes for shared-computer reminders, absence handling, movement
  breaks versus workouts, and the sidebar identity discussion; behavior is proposed
  and not yet implemented.

### Changed

- Set the application target to .NET 10 and added SDK selection through `global.json`.
- Updated the package display name to From the Chair and removed an unused template
  capability.
- Clarified the one-time Developer Mode setup for Visual Studio's packaged Play/F5
  workflow and how it differs from the earlier unpackaged UI smoke check.
- Recorded Windows-account ownership and Start/Snooze/Away as product decisions,
  with proposals for timed pauses, no-response handling, and future profile migration.

This foundation does not yet include active reminders, tray behavior, video playback,
workout sessions, or progress tracking.
