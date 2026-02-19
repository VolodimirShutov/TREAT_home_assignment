using Packages.GameControl.Signals;
using ShootCommon.GlobalStateMachine;
using ShootCommon.InteractiveObjectsSpawnerService;
using Stateless;
using UnityEngine;

namespace Packages.GlobalStates
{
    public class GameWinState: GlobalState
    {
        protected override void Configure()
        {
            Permit<ShowMainMenuState>(StateMachineTriggers.ShowMainMenuState);
        }
        
        protected override void OnEntry(StateMachine<IState, StateMachineTriggers>.Transition transition = null)
        {
            Debug.Log("Game Win");
            InteractiveObjectsManager.Instance.Instantiate("WinPanel", "panels");
            var signal = SignalService.Subscribe<ReturnToMenuSignal>(ReturnToMenu);
            DisposeOnExit.Add(signal);
        }

        private void ReturnToMenu(ReturnToMenuSignal signal)
        {
            Fire(StateMachineTriggers.ShowMainMenuState);
        }
    }
}