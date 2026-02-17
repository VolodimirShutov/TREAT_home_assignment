# Global State Machine

Custom hierarchical finite state machine (HSM) built on top of **Stateless** library, integrated with **Zenject** and **SignalSystem**.

### Key Features

- **Declarative configuration** — all transitions defined in code (no visual editor needed)
- **Hierarchical states** (sub-states) via `.SubstateOf<T>()`
- **Automatic lifecycle** — `OnEntry` / `OnExit` with cleanup via `CompositeDisposable`
- **Signal integration** — fires `ChangeStateSignal` on transition
- **Zenject-friendly** — states bound as singletons, injected into controller
- **Triggers as enum** — `StateMachineTriggers` (extend as needed)
- **Garbage-free after init** (Stateless property)

### Folder Structure
```
ShootCommon/
└── GlobalStateMachine/
    ├── GlobalState.cs           # Base class for all states
    ├── IState.cs
    ├── IStateMachineController.cs
    ├── StateMachineController.cs
    ├── StateMachineTriggers.cs  # enum with triggers
    └── StateMachineContainerExtensions.cs  # Zenject bindings helper
```
### How to Use

1. **Define triggers** (in `StateMachineTriggers.cs`)

```csharp
public enum StateMachineTriggers
{
    Start,          // Boot → Menu
    StartGame,      // Menu → Playing
    GameWon,
    Restart,
    Pause,          // optional
    Resume,
}
```
Create a state (inherit from GlobalState)
```
public class MenuState : GlobalState
{
    protected override void Configure()
    {
        Permit<PlayingState>(StateMachineTriggers.StartGame);
        // або з умовою
        // PermitIf<PlayingState>(StateMachineTriggers.StartGame, () => PlayerNameIsSet());
    }

    protected override void OnEntry(Transition transition)
    {
        // Show menu UI, load difficulty slider, etc.
        SubscribeToSignal<StartGameSignal>(OnStartGameRequested);
    }

    private void OnStartGameRequested(StartGameSignal signal)
    {
        Fire(StateMachineTriggers.StartGame);
    }
}
```

Bind states in Zenject installer

```
public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindState<MenuState>();
        Container.BindState<PlayingState>();
        Container.BindState<FinishedState>();
        // ...

        Container.BindInterfacesAndSelfTo<StateMachineController>()
            .AsSingle()
            .WithArguments(new InitState());  // initial state
    }
}
```

Fire transitions from anywhere

C#// In some MonoBehaviour or service
```
[Inject] private IStateMachineController _stateMachine;

void OnWinCondition()
{
    _stateMachine.Fire(StateMachineTriggers.GameWon);
}
```

React on state change (via signals)

C#SubscribeToSignal<ChangeStateSignal>(OnStateChanged);
```
private void OnStateChanged(ChangeStateSignal signal)
{
    if (signal.SelectedState == StateMachineTriggers.Playing)
    {
        // Start timer, enable input, etc.
    }
}
```
Tips & Best Practices

Use SubscribeToSignal<T>() inside OnEntry — it auto-unsubscribes on exit.
For sub-states (e.g. Paused as sub-state of Playing):C#protected override void Configure()
{
    SubstateOf<PlayingState>();
    Permit<PlayingState>(StateMachineTriggers.Resume);
}
Debug: Debug.Log("Fire " + trigger) already in base class.
To visualize graph (optional): use stateMachine.ToDotGraph() and paste into https://dreampuf.github.io/GraphvizOnline/

This state machine is scalable from prototype to medium-sized game. For this Memory Game assignment — perfect balance between simplicity and power.
text### 2. Оновлений блок для README.md (додай в розділ Architecture / Core Systems)

```markdown
## Architecture Overview

- **Hybrid structure**: feature-first (in `Features/`) + type-based (in `Core/`, `Data/`, etc.)
- **Dependency Injection**: Zenject
- **Event system**: custom SignalSystem (ShootCommon.SignalSystem)
- **Asset management**: Addressables + custom AssetReferenceStorage
- **Global State Machine**: hierarchical FSM based on Stateless (see [GLOBAL_STATE_MACHINE.md](GLOBAL_STATE_MACHINE.md))
  - Controls high-level game flow: Boot → Menu → Playing → Finished
  - Supports sub-states, guards, automatic cleanup
  - Integrated with Zenject & signals

### Folder Structure
```