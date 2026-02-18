using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using FirebaseModul.Configs;
using ShootCommon.SignalSystem;
using Zenject.GameControl.Signals;

namespace Packages.MainMenu
{
    public class MainMenuPresenter : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_InputField playerNameInput;
        [SerializeField] private Slider difficultySlider;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text pairsText;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private Button startButton;

        [Inject] private IGameConfigController _configController;
        [Inject] private SignalService _signalService;

        private const string PLAYER_NAME_KEY = "PlayerName";

        private void Awake()
        {
            ProjectContext.Instance.Container.Inject(this);
        }
        
        [Inject]
        public void Inject()
        {
            playerNameInput.text = PlayerPrefs.GetString(PLAYER_NAME_KEY, "");

            difficultySlider.minValue = 0;
            difficultySlider.maxValue = _configController.Config.levels.Count - 1;
            difficultySlider.wholeNumbers = true;
            difficultySlider.value = 0;

            UpdateDifficultyInfo((int)difficultySlider.value);

            difficultySlider.onValueChanged.AddListener(OnDifficultyChanged);

            startButton.onClick.AddListener(OnStartClicked);
        }

        private void OnDifficultyChanged(float value)
        {
            int levelIndex = Mathf.RoundToInt(value);
            UpdateDifficultyInfo(levelIndex);
        }

        private void UpdateDifficultyInfo(int levelIndex)
        {
            int levelNumber = levelIndex + 1;
            var levelData = _configController.GetLevelConfig(levelNumber);

            levelText.text = $"Level: {levelNumber}";
            pairsText.text = $"Pairs: {levelData.pairs}";
            timeText.text = $"Time per pair: {levelData.timePerPair:F1}s";
        }

        private void OnStartClicked()
        {
            string playerName = playerNameInput.text.Trim();
            
            if (string.IsNullOrEmpty(playerName))
            {
                playerName = _configController.Config.default_player_name;
            }

            PlayerPrefs.SetString(PLAYER_NAME_KEY, playerName);
            PlayerPrefs.Save();

            int selectedLevel = Mathf.RoundToInt(difficultySlider.value) + 1;
            var levelData = _configController.GetLevelConfig(selectedLevel);

            _signalService.Send(new StartGameSignal
            {
                PlayerName = playerName,
                SelectedLevel = selectedLevel,
                PairsCount = levelData.pairs,
                TimePerPair = levelData.timePerPair
            });
            
            Destroy(gameObject);
            Destroy(this);
        }
        
        
    }

}