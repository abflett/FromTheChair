# App overview

From the Chair is a native Windows desktop companion that will encourage simple
movement breaks and calisthenics while someone is working or playing on their computer.

The initial audience is the owner. The aim is to make the app polished enough for
public distribution later, without requiring an account or a hosted service.

## Current foundation

- A WinUI window with Home, Routines, Progress, and Settings navigation.
- Standard controls, a Mica backdrop, and system light/dark styling.
- A preferred break interval that can be explicitly saved to local SQLite storage.
- Whole-minute intervals from 15 to 240 minutes, with a default of 60.
- Save feedback and errors when preferences cannot be loaded or saved.
- Empty states for routines and progress that accurately describe their status.

**Saving an interval does not schedule reminders. Closing the window exits the app.**
There is no tray integration, autostart, alarm, workout timer, video playback, or
results tracking yet.

## Planned capabilities

These describe the product direction, not a commitment to build everything at once.

- Optional startup with Windows and operation from the system tray.
- Native reminders with Start, Snooze, and Away actions, a timed pause after Away,
  and distinct tracking for explicit responses and unanswered reminders.
- A way for someone else using the computer to report the intended user away without
  recording an exercise decision on that person's behalf.
- Separate handling for frequent light movement breaks and planned strength/cardio
  workouts, including absence and missed-session behavior. See the
  [reminder design discussion](plans/reminders-and-activity.md) for proposed semantics.
- Simple, replaceable exercises and routines, with room for more demanding options.
- A workout timer with start, pause, and other session controls.
- Embedded exercise video, including looping and controls independent of the timer.
  YouTube links and offline video are desired; media sourcing and playback details
  still need decisions.
- Session results, optional weight and body measurements, and useful progress charts.
- Settings and routines that fit different schedules and lifestyles. Initially one
  exercise identity per Windows account; multiple local profiles are deferred with
  a migration path planned.

## Established constraints

- **Windows first:** C# / .NET 10, WinUI 3, and Windows App SDK with single-project
  MSIX packaging. A future native Mac port can be considered separately.
- **Simple architecture:** MVVM with separate business rules and persistence,
  within one desktop application.
- **Local data:** SQLite through Microsoft.Data.Sqlite. No application network
  requests are made by the current foundation.
- **Light idle operation:** target below 200 MB resident memory, with little CPU
  use while waiting. Video playback may use more. This is a target to measure
  later, not a verified performance claim.
- **Native feel:** retain the Windows 11 character and familiar behavior while
  refining the app's visual design.
- **User-directed verification:** the user runs the app and tests; agents make,
  document, commit, and push changes.

The next step is [design and layout](plans/design-direction.md), before feature expansion.
