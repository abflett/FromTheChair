# Reminders, presence, and activity types

**Status:** Product discussion. The scenarios and requested capabilities below come
from the user; the proposed behavior still needs agreement. Nothing here is implemented.

## User scenario and requirements

Adam leaves his Windows session open. His wife uses the same computer for YouTube
while he is away, and an exercise reminder intended for Adam appears. She needs a way
to silence it without recording a snooze, skipped exercise, or workout result as Adam.

- Identify the intended person in a reminder.
- Account for absence, sleep, and logoff separately from an exercise decision.
- Keep missed exercise opportunities visible and consider how to make them up.
- Support two activity categories: frequent light movement breaks and less frequent,
  more demanding workouts for strength or conditioning.
- Keep reminders and progress useful without creating confusing account management.

## Proposed identity and presence model

Start with the Windows account as the data owner. The sidebar can show its display
name and initial. Multiple local exercise profiles remain an open, deferred choice;
the immediate scenario concerns a visitor using the owner's session.

Windows session state and computer activity do not establish who is at the keyboard.
A visitor can keep a session active. Provide an explicit **Adam is away** action,
with wording adapted to the displayed name.

That action would silence the current alert, pause further reminders for Adam, and
record an availability event. It must not count as Adam snoozing, skipping, or
completing an exercise. Show the paused state in the app/tray and provide an explicit
**Resume reminders** action when Adam returns. Keyboard activity alone should not
undo an explicit away setting.

This prevents accidental misclassification; it is not authentication or protection
against someone deliberately editing data in a shared Windows session.

## Proposed reminder actions

Keep the primary prompt small. Exact labels, durations, and placement are undecided.

| Action | Intended meaning |
| --- | --- |
| Start | Begin the suggested break or workout; starting is not completion |
| Snooze | The intended user asks for a later reminder |
| Adam is away | Someone reports the intended user unavailable; pause delivery |
| Pause until... | Temporarily suspend reminders, with a clear resume time |
| Skip this workout | Explicitly decline this planned workout, if offered |

A dismissed or unanswered notification is unknown, not proof of a skipped or
completed workout. Keep notification delivery, availability, user choices, and actual
exercise results distinct in storage and any eventual charts. Do not infer exercise
from idle time either.

## Two activity categories and catch-up proposal

| Category | Purpose | Scheduling proposal | After an absence |
| --- | --- | --- | --- |
| Movement breaks | Interrupt time at the computer with walking, mobility, or light activity | Configurable cadence during eligible computer use | Resume a fresh cadence; do not accumulate a backlog of breaks |
| Workouts | Planned strength, cardio, or conditioning sessions | Separate schedule and session plan | Keep unfinished sessions visible and offer rescheduling |

The distinction is the session's purpose and intensity, not a rigid list of exercises.
The same exercise may appear in more than one kind of routine.

For example, returning after three hours away should not produce three simultaneous
movement-break alarms. A planned workout that became due during that time could
appear once as **Workout still planned for today**, with start/reschedule choices.
Automatic extra repetitions, longer sessions, or stacked workouts are not proposed
as compensation for time away.

## Lifecycle questions to settle before implementation

- Suppress alerts while Windows is locked, asleep, or another session is active.
- Logoff or app exit can stop the process. Reconcile persisted schedule information
  on the next launch; do not assume a background timer kept running.
- Decide the return grace period and whether an automatically paused movement cadence
  restarts or preserves its remaining time. Explicitly reported absence should
  continue until the user resumes reminders.
- Decide how idle detection behaves during games, video, and reading; absence cannot
  be inferred reliably from input inactivity alone.
- Decide how a workout already in progress behaves across sleep or absence, including
  independently pausing its media and activity timer.
- Define the rescheduling window, rest days, timezone/clock changes, and whether a
  completed workout resets the next movement break. Avoid overlapping prompts.

## Exercise guidance and product claims

The product should support consistent movement and appropriately scaled training,
without promising that following reminders guarantees a particular level of fitness.
The current 60-minute preference is a software default, not a medical recommendation
or a claim that hourly breaks offset a very long day of sitting.

WHO guidance supports reducing sedentary time and regular aerobic and strengthening
activity. It does not establish one universally appropriate hourly reminder routine.
Exercise selection, progression, and recovery need their own design work before
shipping a prescribed program. See [WHO's physical activity guidance](https://www.who.int/news-room/fact-sheets/detail/physical-activity).

The current implementation priority remains sidebar and visual design. Use this plan
to inform those choices, rather than implementing a scheduler during the design pass.
