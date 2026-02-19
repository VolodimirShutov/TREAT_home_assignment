using Packages.GameControl.Signals;
using ShootCommon.SignalSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace Packages.GameControl
{
    public class MovesPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text movesText;

        [Inject] private GameController _gameController;
        [Inject] private SignalService _signalService;

        private CompositeDisposable _disposeOnExit = new CompositeDisposable();
        
        private void Awake()
        {
            ProjectContext.Instance.Container.Inject(this);
            var nt_signal = _signalService.Subscribe<NewTrySignal>(OnNewTry);
            _disposeOnExit.Add(nt_signal);
            UpdateMovesUI();
        }

        private void OnNewTry(NewTrySignal signal)
        {
            UpdateMovesUI();
        }

        private void UpdateMovesUI()
        {
            movesText.text = $"Moves: {_gameController.Try}";
        }

        private void OnDestroy()
        {
            _disposeOnExit.Dispose();
        }
    }
}