using ShootCommon.GlobalStateMachine;

namespace Packages.GlobalStates
{
    public class RestartGameState: GlobalState
    {
        protected override void Configure()
        {
            Permit<GameState>(StateMachineTriggers.GameState);
        }
    }
}