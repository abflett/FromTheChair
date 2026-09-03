# Design direction and next steps

**Status:** Design discussion next; detailed visual choices are open.

## Agreed direction

- Keep the native Windows approach and the current Windows 11 character.
- Aim for a simple, modern app that is comfortable to leave available during work
  or games.
- Refine the feel and structure before adding more content or functionality.
- Preserve familiar Windows controls, keyboard access, and system theme support.
- Keep the eventual workout screen focused on the exercise, timer, and video controls.

The existing navigation and page layouts are a starting point, not a final design
commitment. No replacement layout, palette, typography, or branding has been selected.

## Questions for the design discussion

- **Overall structure:** which destinations belong in navigation, and what should
  be immediately visible when the app opens?
- **Home:** should it center on the next break, a quick-start workout, or a quiet summary?
- **Density and sizing:** a compact utility window, a roomier dashboard, or different
  layouts for setup and active exercise?
- **Visual language:** spacing, typography, surfaces, accent use, and how much of the
  system styling to retain.
- **Workout view:** placement of video, exercise instructions, timer, and independent
  playback/session controls.
- **Window behavior:** how a normal window, a possible compact workout view, and
  future tray operation should relate.

These are discussion prompts, not decided requirements. Record the user's choices
here as they are made.

## Suggested sequence

1. Establish the feel and layout using the user's references and preferences.
2. Apply the agreed design to the existing shell and preference flow.
3. Let the user try it and provide feedback.
4. Once the design is settled enough, add features in small end-to-end steps.

## Later implementation candidates

Order and scope can change after the design discussion.

- Application lifetime and tray integration, followed by native reminders and
  reminder-response tracking.
- A workout session with a timer and independently controlled embedded video.
- Saved session results, optional body measurements, and progress charts.
- More settings, exercise options, and profiles as actual use exposes a need.

Video source handling, notification actions, sleep/resume behavior, autostart choices,
and release/distribution details remain open. Cross-platform work is deferred.
