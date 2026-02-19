using Packages.GameControl.Signals;
using ShootCommon.SignalSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Packages.GameControl.WinLose
{
    public class WinPanel : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text playerNameText;   
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text movesText;
        [SerializeField] private TMP_Text timeLeftText;
        [SerializeField] private Button okButton;

        [Inject] private GameController _gameController;
        [Inject] private SignalService _signalService;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            ProjectContext.Instance.Container.Inject(this);
            /*
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            _canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
            */
            okButton.onClick.AddListener(OnOkClicked);
            ShowWinPanel();
        }

        public void ShowWinPanel()
        {
            gameObject.SetActive(true);

            playerNameText.text = $"Player: {_gameController.GameModel.PlayerName}";
            levelText.text = $"Level: {_gameController.GameModel.SelectedLevel}";
            movesText.text = $"Moves: {_gameController.GameModel.Try}";

            float remainingTime = _gameController.GameModel.TimePerPair - _gameController.GameModel.ElapsedTime;
            timeLeftText.text = $"Time left: {remainingTime:F1}s";
        }

        private void OnOkClicked()
        {
            _signalService.Send(new ReturnToMenuSignal());
            
            Destroy(gameObject);
            Destroy(this);
        }
    }

}