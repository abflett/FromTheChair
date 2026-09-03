# Reminders, presence, and activity types

**Status:** Windows-account ownership and the Start / Snooze / Away actions are agreed.
Timed Away and an automatic pause after no response are the desired direction; exact
durations and interactions below remain proposals. Nothing here is implemented.

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

## Identity and presence

Use the Windows account as the data owner, showing its display name and initial in
the sidebar. Multiple exercise users within one Windows account are out of scope for
the initial version. Preserve a migration path for local profiles if that changes;
see [the ownership plan](../architecture.md#planned-data-ownership).

Windows session state and computer activity do not establish who is at the keyboard.
A visitor can keep a session active. Provide an explicit **Away** action in a reminder
addressed to Adam, with wording adapted to the displayed name where helpful.

Away silences the current alert, pauses further reminders for Adam, and records an
availability event. It must not count as Adam snoozing, skipping, or completing an
exercise. The pause should have an expiry so the app can try again later instead of
being forgotten in the tray. Show **Paused until [time]** in the app/tray and provide
**Resume now**. Keyboard activity alone should not shorten an explicit Away interval.

This prevents accidental misclassification; it is not authentication or protection
against someone deliberately editing data in a shared Windows session.

## Reminder actions

The agreed primary actions are **Start**, **Snooze**, and **Away**. Keep the primary
prompt small. Durations and the placement of secondary actions remain undecided.

| Action | Intended meaning |
| --- | --- |
| Start | Begin the suggested break or workout; starting is not completion |
| Snooze | The intended user asks for a later reminder |
| Away | Report the intended user unavailable, then choose how long to pause |

An explicit **Skip this workout** action may belong on the workout screen later;
it is not another primary reminder button.

## Timed Away proposal

- Silence the current sound immediately when Away is selected, then show a compact
  duration choice. Do not keep sounding while someone answers the second step.
- Suggested choices: **30 minutes**, **2 hours**, **4 hours**, and **Until tomorrow**.
  Use 2 hours as a provisional default. These values are not finalized.
- Establish the default pause immediately; a duration choice replaces it. Closing
  the duration chooser should leave the default pause in effect, not restart alarms
  or leave an unbounded pause. Show the resulting expiry clearly.
- Persist the pause reason and expiry across restart. When the interval expires,
  re-evaluate eligibility before issuing at most one relevant reminder. Sleeping,
  locked, inactive Windows sessions and configured quiet hours still suppress alerts.
- Define tomorrow as the next permitted reminder window in local time, rather than
  midnight. Working-hour and quiet-hour settings still need design.

## No-response proposal

The user suggested pausing for 2–4 hours after an unanswered reminder. Start with a
configurable 2-hour automatic pause, with 4 hours available as an option.

Use a separate, defined response deadline; its duration is still open. The native
banner disappearing is not that deadline and does not establish that the user saw
it. Once the deadline passes without an action, stop any alert sound, record
**No response / automatically paused**, and wait until the pause expires before
trying again. Do not relabel this as the user explicitly selecting Away.

There should be no repeated sounds during the response window. A later retry must
still respect the current schedule and availability. A fresh unanswered retry can
pause again without accumulating old reminders. Expire superseded notification
actions so they cannot change the current schedule.

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
  restarts or preserves its remaining time. A timed Away interval ends at its expiry
  or when the user selects Resume now; unlocking alone does not end it early.
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
