# Project Structure

The project uses a **feature-first + type-based** hybrid organization with **alphabetical sorting** of folders at every level (A–Z).  
This approach makes navigation fast and intuitive even as the project grows.

All important documentation files are placed near the relevant code or in the root.
Assets/
├── Project/                              # Main project content (sorted first due to name)
│   ├── Addressables/                     # Addressables groups & build settings
│   │   ├── Groups/
│   │   └── Scenes/
│   │
│   ├── Art/                              # All visual assets
│   │   ├── Atlases/                      # Sprite Atlas assets
│   │   ├── Effects/                      # VFX textures, particles
│   │   ├── Sprites/                      # Source sprites (.png, .psd, etc.)
│   │   ├── Tiles/                        # Tilemap tiles (if used)
│   │   └── UI/                           # UI icons, buttons, panels
│   │
│   ├── Audio/                            # Audio assets
│   │   ├── Music/
│   │   ├── SFX/
│   │   └── UI/
│   │
│   ├── Packages/                         # All project-specific C# code
│   │   ├── Common/                       # Shared utilities & helpers
│   │   │   ├── Extensions/
│   │   │   ├── Math/
│   │   │   └── Tools/
│   │   │
│   │   ├── Core/                         # Core systems & infrastructure
│   │   │   ├── Managers/
│   │   │   ├── Services/
│   │   │   ├── Signals/
│   │   │   └── StateMachine/             # Base classes for global FSM
│   │   │
│   │   ├── Data/                         # ScriptableObjects, configs, runtime data
│   │   │   ├── Configs/
│   │   │   ├── GameSettings/
│   │   │   └── RuntimeData/
│   │   │
│   │   └── GlobalStates/                 # Concrete game states (Init, Menu, Playing, Finished…)
│   │       ├── InitState.cs
│   │       ├── MenuState.cs              # (will be added)
│   │       ├── PlayingState.cs           # (will be added)
│   │       └── FinishedState.cs          # (will be added)
│   │
│   ├── Prefabs/                          # All prefabs
│   │   ├── Interactive/
│   │   ├── Player/
│   │   ├── Projectiles/
│   │   └── UI/
│   │
│   ├── ProjectStructure.md               # ← This file (project folder layout)
│   │
│   ├── Scenes/                           # All game scenes
│   │   ├── Boot.unity                    # Entry point / initialization
│   │   ├── MainMenu.unity
│   │   └── Gameplay.unity                # Main memory game scene
│   │
│   └── Settings/                         # Project-level Unity settings
│       ├── Input/
│       └── URP-HighQuality.asset
│
├── Packages/                             # Unity manifest & resolved packages (auto)
├── ProjectSettings/                      # Project-wide settings (git-friendly)
├── UserSettings/                         # Local editor settings (.gitignore)
└── ShootCommon/                          # Reusable libraries & patterns
├── AssetReferences/                  # Addressables wrapper & loader
├── GlobalStateMachine/               # Custom HSM based on Stateless
│   ├── GlobalState.cs
│   ├── IState.cs
│   ├── IStateMachineController.cs
│   ├── StateMachineController.cs
│   ├── StateMachineTriggers.cs
│   ├── StateMachineContainerExtensions.cs
│   └── GLOBAL_STATE_MACHINE.md       # ← Detailed usage documentation
├── InteractiveObjectsSpawnerService/ # Container-based object spawning
└── SignalSystem                      # Event system (SignalService)
text### Documentation files

- [PROJECT_STRUCTURE.md](./Project/ProjectStructure.md) — overall folder layout & conventions
- [ShootCommon/GlobalStateMachine/GLOBAL_STATE_MACHINE.md](./ShootCommon/GlobalStateMachine/GLOBAL_STATE_MACHINE.md) — how to use the Global State Machine
- [LICENSE](./LICENSE) — MIT license