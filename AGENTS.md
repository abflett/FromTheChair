# Working on From the Chair

These instructions apply throughout this repository. Follow the user's latest
explicit directions if they change the workflow below.

## Working agreement

- After completing requested changes, commit and push them to the current branch's
  upstream. This is standing authorization; do not ask again for routine commits
  and pushes.
- Inspect Git status first. Include the work for the request and preserve unrelated
  user changes. Do not discard changes, force-push, or rewrite shared history.
- The user handles verification on their own time. Do not run tests, launch the app,
  or take control of the desktop. Leave builds and runtime verification to the user
  too, unless they explicitly request otherwise.
- Source inspection and read-only Git checks are fine. Commands documented for the
  user are reference material, not permission for an agent to execute them.
- Do not add automatic test/build workflows as part of an unrelated change.
- Report what changed, whether the commit and push succeeded, and that execution
  checks were left to the user. Never claim unperformed checks passed.
- Keep changes small and understandable. Use reasonable judgment for routine
  implementation details and ask only when a meaningful product decision is missing.

## Product direction

- Build a native Windows desktop companion for simple movement breaks and
  calisthenics, primarily for personal use with possible public distribution later.
- Use C#, .NET, WinUI 3, and the Windows App SDK. Preserve the native Windows 11
  character, system theme support, and accessible standard controls.
- Keep the app lightweight while idle. Below 200 MB resident memory is a target,
  not an established measurement. Avoid polling, eager media initialization, or
  keeping a player alive after it is no longer needed.
- Keep personal data local by default. Do not introduce accounts, telemetry,
  paid services, or cross-platform frameworks without an agreed need.
- The immediate priority is design and layout before adding more workout content
  or features. See [the design plan](docs/plans/design-direction.md).

## Architecture

- Use a pragmatic modular monolith with MVVM, not a framework-heavy interpretation
  of Clean Architecture.
- `FromTheChair.App` owns WinUI views, view models, startup composition, and Windows
  integration. Keep code-behind focused on presentation and navigation.
- `FromTheChair.Core` owns platform-independent models, rules, and useful interfaces.
  It must not depend on WinUI, SQLite, or Infrastructure.
- `FromTheChair.Infrastructure` implements persistence and references Core.
- App may reference Core and Infrastructure. Wire dependencies at startup using
  ordinary constructor injection; add abstractions only at useful boundaries.
- Preserve stored data. Use explicit schema migrations and parameterized SQL.
  Do not silently reset an unreadable database or overwrite an unsupported schema.
- When implemented, separate scheduling rules from notification delivery, window
  lifetime from tray lifetime, and workout timing from video playback.
- Do not present planned functionality as working or populate tracking with fake
  user results.

## Documentation

- Use [docs/README.md](docs/README.md) as the documentation index.
- Keep the overview and architecture aligned with meaningful implementation changes.
- Record notable changes under `Unreleased` in [the changelog](docs/CHANGELOG.md).
  Add dated release sections when releases actually exist.
- Put proposals and working notes under `docs/plans/`; distinguish accepted decisions,
  open questions, and deferred ideas. Do not turn an unapproved idea into a requirement.
- Keep the root README short and link to detailed documents.
