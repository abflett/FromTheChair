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

### Changed

- Set the application target to .NET 10 and added SDK selection through `global.json`.
- Updated the package display name to From the Chair and removed an unused template
  capability.

This foundation does not yet include active reminders, tray behavior, video playback,
workout sessions, or progress tracking.
