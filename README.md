# Module 1: Possession Mechanic

Core prototype of a body-switching system in Unity - the foundational mechanic for a larger systemic RPG concept

![Gameplay demo](demo.gif)

## What it demonstrates

The player controls a "spirit" that can take control of different bodies in the world at will. This prototype implements the technical foundation:

- Press **E** near a possessable body to switch control to it
- The previous body stops responding to input; the new one becomes controllable
- Camera smoothly transitions to follow the newly possessed body
- Visual feedback (color) shows which body is currently active

## Features

- **Possession system** driven by an `IPossessable` interface - any component can become possessable without shared inheritance, enabling future bodies with completely different capabilities
- **Proximity-based detection** via `Physics.OverlapSphere` on a dedicated layer
- **Camera-relative movement** - the player moves relative to camera orientation, not world axes
- **Smooth camera transitions** using Cinemachine
- Built on Unity's **Input System** 

## Tech stack

- Unity 6 (URP)
- C#
- Unity Input System
- Cinemachine 3.x

## Project structure

```
Assets/
  Scripts/
    PlayerController.cs      — player movement, camera-relative, Input System driven
    NPCIdleController.cs     — minimal possessable body with visual state feedback
    PossessionManager.cs     — possession logic, IPossessable, camera switching
  Scenes/
    SampleScene.unity        — test scene with Player and NPC
```

## How to run

1. Clone the repository
2. Open the project in Unity 6 (or later) with URP
3. Open `Assets/Scenes/SampleScene.unity`
4. Enter Play mode
5. Move with **WASD**, hold **Shift** to run, press **E** near the other body to possess it

## Known limitations (by design, for this module's scope)

- No state transfer between bodies, out of scope for this prototype
- NPC body has no autonomous behavior yet
- No inventory/economy





