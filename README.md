# Module 1: Possession Mechanic

Core prototype of a god-game-to-first-person possession system in Unity - the foundational mechanic for a larger sandbox fantasy world simulator concept

![Gameplay demo](demo.gif)

## What it demonstrates

The player observes and navigates the world from a free-flying top-down camera, then clicks any possessable body to instantly take control of it from a first-person perspective

- Free-flying spirit camera by default
- Click any possessable body to possess it, camera cuts to first-person view of that body
- Each body has its own dedicated first-person camera, dynamically activated on possession
- Press **Esc** to release the body and return to the spirit camera


## Features

- **Possession system** driven by an `IPossessable` interface - any component can become possessable and provide its own camera without shared inheritance, enabling future bodies with completely different capabilities (blacksmith, mage, guard and etc)
- **Raycast-based possession** - click detection via `Physics.Raycast` from the spirit camera through the cursor position, on a dedicated `Possessable` layer
- **Free-flying spirit camera** - horizontal movement independent of view angle, scroll-wheel zoom with height clamping
- **First-person body control** - mouse look with separated yaw and pitch, movement relative to the body's own facing direction
- **Per-body camera architecture** - each possessable body owns and exposes its own camera object, avoiding a single hardcoded camera reference
- Built on Unity's **Input System**

## Controls

| Context | Input | Action |
|---|---|---|
| Spirit mode | WASD | Move camera |
| Spirit mode | Mouse scroll | Zoom in/out |
| Spirit mode | Left click | Possess body under cursor |
| Possessed | WASD | Move body |
| Possessed | Mouse | Look (yaw/pitch) |
| Possessed | Left Shift | Run |
| Possessed | Esc | Release body, return to spirit mode |

## Tech stack

- Unity 6 (URP)
- C#
- Unity Input System (multiple Action Maps: `SpiritControls`, `CharacterControls`, `GlobalControls`)
- [Cinemachine 3.x] currently not used, keeping it just in case. Probably will need to replace first person cameras with it

## Project structure

```
Assets/
  Scripts/
    SpiritCameraController.cs   - free-fly top-down camera, movement + zoom
    FirstPersonController.cs    - possessed body control, camera-relative movement, mouse look
    NPCIdleController.cs         - minimal possessable body with visual state feedback
    PossessionManager.cs        - IPossessable, camera switching
  Scenes/
    SampleScene.unity           - test scene with a possessable Player-like body and an NPC
```

## How to run

1. Clone the repository
2. Open the project in Unity 6 with URP
3. Open `Assets/Scenes/SampleScene.unity`
4. Enter Play mode
5. Fly the spirit camera with **WASD** and mouse scroll, **left-click** a body to possess it, **Esc** to release

## Known limitations (by design, for this module's scope)

- No state transfer between bodies, out of scope for this prototype
- NPC body has no autonomous behavior yet
- No inventory/economy
- No true world simulation at the macro level yet 

## Design note

This module went through a significant architecture revision mid-development: the original concept (a "wandering spirit" narrative, third-person approach-based possession) was replaced with a faceless top-down observer and click-to-possess first-person control, better matching the sandbox god-game direction of the overall project


