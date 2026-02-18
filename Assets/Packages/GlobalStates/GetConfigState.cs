using FirebaseModul.FirebaseSignals;
using ShootCommon.GlobalStateMachine;
using Stateless;
using UnityEngine;

namespace Packages.GlobalStates
{
    public class GetConfigState: GlobalState
    {
        protected override void Configure()
        {
            Permit<ShowMainMenuState>(StateMachineTriggers.ShowMainMenuState);
        }

        protected override void OnEntry(StateMachine<IState, StateMachineTriggers>.Transition transition = null)
        {
            Debug.Log("OnEntry GetConfigState");
            var signal = SignalService.Subscribe<FirebaseConfigUpdatedSignal>(FirebaseConfigUpdated);
            DisposeOnExit.Add(signal);
            SignalService.Send(new FirebaseUpdateConfigSignal());
        }

        private void FirebaseConfigUpdated(FirebaseConfigUpdatedSignal signal)
        {
            Debug.Log($"FirebaseConfigUpdated: {signal}");
            Fire(StateMachineTriggers.ShowMainMenuState);
        }
    }
}