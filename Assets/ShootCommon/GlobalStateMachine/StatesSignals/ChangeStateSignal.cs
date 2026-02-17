using ShootCommon.SignalSystem;

namespace ShootCommon.GlobalStateMachine.StatesSignals
{
    public class ChangeStateSignal : Signal
    {
        public StateMachineTriggers SelectedState;
    }
}