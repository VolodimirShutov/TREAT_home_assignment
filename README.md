# Memory Game – TREAT Home Assignment

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) [![Unity 2022+](https://img.shields.io/badge/Unity-2022+-000.svg?logo=unity)](https://unity.com)

Memory card matching game with difficulty scaling, timer, move counter, Firebase integration and win animation — completed as a home assignment for **TREAT** Unity Developer position.

## Project Structure & Documentation

- Full folder layout & naming conventions → [PROJECT_STRUCTURE.md](./Assets/Project/ProjectStructure.md)
- Global State Machine (HSM based on Stateless) → [GLOBAL_STATE_MACHINE.md](./Assets/ShootCommon/GlobalStateMachine/GLOBAL_STATE_MACHINE.md)
- License → [LICENSE](./LICENSE)

## Features Implemented

- Difficulty slider (7 levels: 2 to 8 cards to match)
- Player name input & saving
- Scaled card grid depending on difficulty
- Timer per pair (5–40 seconds)
- Move counter
- Restart button
- Finish screen with total moves & simple win animation (particle system / scale + fade)
- Firebase integration for storing name + difficulty + score

## Bonus Features (planned / partially done)

- Scalable difficulty beyond 7 levels
- High-score storage & display
- Leaderboard page
- Card flip animations
- Visual effects on match / mismatch

## Tech Stack

- Unity (URP 2D)
- Zenject (DI)
- Stateless + custom wrapper (Global State Machine)
- SignalSystem (event bus)
- Addressables
- Firebase (Authentication & Firestore / Realtime Database)

## How to Run

1. Open project in Unity 2022.3+ (URP recommended)
2. Make sure Firebase is configured (see `Assets/Firebase/` or README-Firebase.md if present)
3. Play `Boot.unity` scene

Good luck reviewing! Questions / feedback welcome.