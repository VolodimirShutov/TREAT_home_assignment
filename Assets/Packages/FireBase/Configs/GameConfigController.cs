using System;
using System.Collections.Generic;
using Firebase;
using Firebase.RemoteConfig;
using UnityEngine;
using Zenject;
using ShootCommon.SignalSystem;
using Cysharp.Threading.Tasks;
using FirebaseModul.FirebaseSignals;

namespace FirebaseModul.Configs
{
    public class GameConfigController : IGameConfigController, IDisposable
    {
        private SignalService _signalService;
        [Inject] private GameDifficultyConfig _defaultConfig; // ScriptableObject з дефолтними рівнями

        public ConfigsModel Config { get; private set; } = new ConfigsModel();

        private IDisposable _updateSubscription;

        [Inject]
        public void Init(SignalService signalService)
        {
            _signalService = signalService;
            // Subscribe to manual update requests from states
            _updateSubscription = _signalService.Subscribe<FirebaseUpdateConfigSignal>(OnUpdateRequested);

            // Load default config from ScriptableObject on start
            LoadDefaultFromScriptableObject();

            Debug.Log("[GameConfigController] Initialized with default ScriptableObject config");
        }

        public void RequestConfigUpdate()
        {
            _ = UpdateConfigAsync();
        }

        private void OnUpdateRequested(FirebaseUpdateConfigSignal signal)
        {
            Debug.Log("[GameConfigController] Config update requested via signal");
            _ = UpdateConfigAsync();
        }

        private async UniTaskVoid UpdateConfigAsync()
        {
            Debug.Log("[GameConfig] Attempting to fetch remote config...");

            bool firebaseSuccess = false;

            try
            {
                // Check Firebase dependencies
                var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
                if (dependencyStatus != DependencyStatus.Available)
                {
                    Debug.LogWarning($"[GameConfig] Dependencies not available: {dependencyStatus}");
                }
                else
                {
                    var rc = FirebaseRemoteConfig.DefaultInstance;

                    // Set minimal defaults (empty levels)
                    var defaults = new Dictionary<string, object>
                    {
                        { "difficulty_config", JsonUtility.ToJson(new ConfigsModel()) }
                    };
                    await rc.SetDefaultsAsync(defaults);

                    // Fetch and activate
                    await rc.FetchAndActivateAsync();

                    var json = rc.GetValue("difficulty_config").StringValue;
                    ParseConfig(json);

                    if (Config.levels.Count > 0)
                    {
                        firebaseSuccess = true;
                        Debug.Log($"[GameConfig] Remote config loaded successfully — {Config.levels.Count} levels");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[GameConfig] Remote config fetch failed: {ex.Message}");
            }

            // If remote failed or returned empty data → fallback to ScriptableObject
            if (!firebaseSuccess)
            {
                Debug.Log("[GameConfig] Falling back to ScriptableObject default config");
                LoadDefaultFromScriptableObject();
            }

            // Notify the game that config is ready (remote or fallback)
            _signalService.Send(new FirebaseConfigUpdatedSignal());
        }

        private void LoadDefaultFromScriptableObject()
        {
            Config.levels.Clear();

            foreach (var lvl in _defaultConfig.levels)
            {
                Config.levels.Add(new ConfigModel
                {
                    level = lvl.level,
                    pairs = lvl.pairs,
                    timePerPair = lvl.timePerPair
                });
            }

            Config.min_pairs = _defaultConfig.minLevel;
            Config.max_pairs = _defaultConfig.maxLevel;
            Config.timer_curve = 1.2f; // або додай поле в ScriptableObject
            Config.default_player_name = _defaultConfig.defaultPlayerName;

            Debug.Log($"[GameConfig] Default config loaded from ScriptableObject — {Config.levels.Count} levels");
        }

        private void ParseConfig(string json)
        {
            try
            {
                var parsed = JsonUtility.FromJson<ConfigsModel>(json);
                if (parsed != null && parsed.levels != null && parsed.levels.Count > 0)
                {
                    Config = parsed;
                }
                else
                {
                    Debug.LogWarning("[GameConfig] Remote config parsed but empty — using default");
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[GameConfig] Remote JSON parse failed: {ex.Message}");
            }
        }

        public ConfigModel GetLevelConfig(int level)
        {
            level = Mathf.Clamp(level, 1, Config.levels.Count);
            return Config.levels[level - 1];
        }

        public void Dispose()
        {
            _updateSubscription?.Dispose();
        }
    }
}