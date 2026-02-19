using Cysharp.Threading.Tasks;
using Packages.GameControl.Signals;
using ShootCommon.GlobalStateMachine;
using ShootCommon.InteractiveObjectsSpawnerService;
using Stateless;

namespace Packages.GlobalStates
{
    public class ShowMainMenuState: GlobalState
    {
        
        protected override void Configure()
        {
            Permit<InitStartGame>(StateMachineTriggers.InitStartGame);
        }
        
        protected override void OnEntry(StateMachine<IState, StateMachineTriggers>.Transition transition = null)
        {
            InteractiveObjectsManager.Instance.Instantiate("MainMenu", "MainMenuUi");
            var signal = SignalService.Subscribe<StartGameSignal>(StartGame);
            DisposeOnExit.Add(signal);
        }

        private void StartGame(StartGameSignal signal)
        {
            DelayAndFire().Forget();
        }
        
        private async UniTaskVoid DelayAndFire()
        {
            await UniTask.NextFrame();
            Fire(StateMachineTriggers.InitStartGame);
        }
    }
}