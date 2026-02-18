using Packages.GlobalStates.StatesSignals;
using ShootCommon.GlobalStateMachine;
using Stateless;

namespace Packages.GlobalStates
{
    public class InitState : GlobalState
    {
        protected override void Configure()
        {
            Permit<StartState>(StateMachineTriggers.StartState);
        }

    }
}