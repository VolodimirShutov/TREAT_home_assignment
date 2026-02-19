using TMPro;
using UnityEngine;
using Zenject;

namespace Packages.GameControl
{
    public class TimerPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;

        [Inject] private GameController _gameController;

        private bool _isRunning = false;

        private void Awake()
        {
            ProjectContext.Instance.Container.Inject(this);
            timerText.text = "Time: 0.0s";
            
            StartTimer();
        }

        public void StartTimer()
        {
            _isRunning = true;
        }

        private void Update()
        {
            if (!_isRunning) return;

            _gameController.TickTimer(Time.deltaTime);

            float remaining = _gameController.GameModel.TimePerPair - _gameController.GameModel.ElapsedTime;
            timerText.text = $"Time: {remaining:F1}s";

            if (remaining <= 0)
            {
                _isRunning = false;
                timerText.text = "Time's up!";
            }
        }

        public void StopTimer()
        {
            _isRunning = false;
        }
    }
}