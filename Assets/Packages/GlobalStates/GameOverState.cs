using ShootCommon.GlobalStateMachine;

namespace Packages.GlobalStates
{
    public class GameOverState: GlobalState
    {
        
        protected override void Configure()
        {
            Permit<ShowMainMenuState>(StateMachineTriggers.ShowMainMenuState);
        }
    }
}