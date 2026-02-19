using Cysharp.Threading.Tasks;
using Packages.GameControl.Signals;
using ShootCommon.GlobalStateMachine;
using ShootCommon.InteractiveObjectsSpawnerService;
using Stateless;

namespace Packages.GlobalStates
{
    public class GameOverState: GlobalState
    {
        protected override void Configure()
        {
            Permit<ShowMainMenuState>(StateMachineTriggers.ShowMainMenuState);
        }
        
        protected override void OnEntry(StateMachine<IState, StateMachineTriggers>.Transition transition = null)
        {
            InteractiveObjectsManager.Instance.Instantiate("LoosePanel", "panels");
            var signal = SignalService.Subscribe<ReturnToMenuSignal>(ReturnToMenu);
            DisposeOnExit.Add(signal);
        }

        private void ReturnToMenu(ReturnToMenuSignal signal)
        {
            Fire(StateMachineTriggers.ShowMainMenuState);
        }
    }
}