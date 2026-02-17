using ShootCommon.GlobalStateMachine;

namespace Packages.GlobalStates
{
    public class InitState : GlobalState
    {
        protected override void Configure()
        {
            //Permit<StartState>(StateMachineTriggers.Start);
        }
    }
}