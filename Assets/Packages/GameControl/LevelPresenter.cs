using Packages.GameControl.GameSignals;
using Packages.GameControl.Signals;
using ShootCommon.SignalSystem;
using UnityEngine;
using Zenject;

namespace Packages.GameControl
{
    public class LevelPresenter: MonoBehaviour
    {
        [Inject] private SignalService _signalService;
        private CompositeDisposable _disposeOnExit = new CompositeDisposable();

        private void Awake()
        {
            ProjectContext.Instance.Container.Inject(this);
            var gw_signal = _signalService.Subscribe<GameWinSignal>(GameWin);
            var go_signal = _signalService.Subscribe<GameOverSignal>(GameOver);
            _disposeOnExit.Add(gw_signal);
            _disposeOnExit.Add(go_signal);
        }

        private void GameWin(GameWinSignal signal)
        {
            DestroyPanel();
        }

        private void GameOver(GameOverSignal signal)
        {
            DestroyPanel();
        }

        private void DestroyPanel()
        {
            _disposeOnExit.Dispose();
            Destroy(gameObject);
            Destroy(this);
        }
        
        
    }
}