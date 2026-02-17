using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace ShootCommon.SignalSystem.Editor
{
    public class SignalDebuggerWindow : EditorWindow
    {
        private List<string> _cachedSignalHistory = new();
        private Dictionary<Type, List<Delegate>> _cachedSubscriptions = new();
    
        [MenuItem("Tools/Shoot/Signal Debugger")]
        public static void ShowWindow()
        {
            GetWindow<SignalDebuggerWindow>("Signal Debugger");
        }

        private void OnGUI()
        {
            if (Application.isPlaying)
            {
                _cachedSubscriptions = SignalService.Instance.GetActiveSubscriptions();
                _cachedSignalHistory = SignalService.Instance.GetSignalHistory();
            }
        
            GUILayout.Label("Active Subscriptions", EditorStyles.boldLabel);
            foreach (var entry in _cachedSubscriptions)
            {
                GUILayout.Label($"{entry.Key.Name}: {entry.Value.Count} handlers");
            }
        
            GUILayout.Space(10);
            GUILayout.Label("Last 10 Signals", EditorStyles.boldLabel);
            foreach (var signal in _cachedSignalHistory)
            {
                GUILayout.Label(signal);
            }
        }
    }
}

#endif