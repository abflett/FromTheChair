# Development

## Workflow

The user runs the app and tests on their own time and reports results. Agents should
inspect source, make requested changes, update relevant docs, and commit and push
completed work to the current branch's upstream.

Agents must not launch the app, run tests, take control of the desktop, or execute
builds for verification unless the user explicitly requests it.
The commands below are **reference instructions for the user**, not an automatic
verification requirement. See [AGENTS.md](../AGENTS.md) for the standing agreement.

Record what was actually checked. Do not describe unexecuted builds, tests, or
runtime behavior as verified.

## Visual Studio

Use Visual Studio 2026 with the WinUI/Windows App SDK development tools and a .NET 10
SDK. `global.json` accepts installed stable .NET 10 feature bands. Package versions
are recorded in the project files.

1. Open `FromTheChair.slnx`.
2. Set `FromTheChair.App` as the startup project.
3. Select **Debug / x64** and **FromTheChair.App (Package)**.
4. Press **F5** when ready to run the app.

The app uses single-project MSIX packaging; there is no separate packaging project.

## Manual command-line build and tests

From PowerShell in the repository root:

```powershell
dotnet build FromTheChair.slnx -p:Platform=x64
dotnet test FromTheChair.Tests/FromTheChair.Tests.csproj
```

Existing tests cover preference validation and SQLite persistence behavior, including
refusing unsupported schemas and unreadable databases. They use temporary databases
and do not launch the UI or modify application preferences.

For an unpackaged development build:

```powershell
dotnet build FromTheChair.App/FromTheChair.App.csproj -p:Platform=x64 -p:WindowsPackageType=None
```

Selecting the unpackaged launch profile alone does not change the packaging properties.
The generated executable is under the app's `bin/x64/Debug` directory and requires
the matching Windows App Runtime. The packaged Visual Studio workflow is the default.
These build commands do not publish or sign a release.

## Manual checks when useful

These are optional prompts for the user's own testing, not a required agent step.

- Navigate between Home, Routines, Progress, and Settings.
- Save an interval between 15 and 240 minutes, reopen the app, and check it persists.
- Check that invalid or fractional intervals cannot be saved.
- Check layout at the window sizes, display scaling, and system themes you use.
- Report which page/action failed, what happened, and any visible error text.

The preference database lives at
`%LOCALAPPDATA%\FromTheChair\preferences.db`. Packaged and unpackaged runs share it
for the same Windows user. Do not delete it as a routine troubleshooting step.

## Documentation and changes

Keep implementation status accurate in [the overview](overview.md), update
[architecture notes](architecture.md) when boundaries change, and add notable changes
to [Unreleased](CHANGELOG.md). Put proposals and open decisions in
[`plans/`](plans/design-direction.md).

The current next step is the visual design discussion. Tray/reminder, workout/media,
and tracking work follow once the initial design direction is established.
