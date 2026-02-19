using Packages.GameControl.GameSignals;
using Packages.GameControl.Signals;
using ShootCommon.GlobalStateMachine;
using Stateless;

namespace Packages.GlobalStates
{
    public class GameState: GlobalState
    {
        protected override void Configure()
        {
            Permit<GameOverState>(StateMachineTriggers.GameOverState);
            Permit<RestartGameState>(StateMachineTriggers.RestartGameState);
        }
        
        
        protected override void OnEntry(StateMachine<IState, StateMachineTriggers>.Transition transition = null)
        {
            var r_signal = SignalService.Subscribe<RestartGameSignal>(RestartGame);
            var go_signal = SignalService.Subscribe<GameOverSignal>(GameOver);
            DisposeOnExit.Add(r_signal);
            DisposeOnExit.Add(go_signal);
        }

        private void RestartGame(RestartGameSignal signal)
        {
            Fire(StateMachineTriggers.RestartGameState);
        }
        
        private void GameOver(GameOverSignal signal)
        {
            Fire(StateMachineTriggers.GameOverState);
        }
    }
}