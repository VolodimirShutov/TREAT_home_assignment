using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDifficultyConfig", menuName = "TREAT/Game/Difficulty Config", order = 1)]
public class GameDifficultyConfig : ScriptableObject
{
    [Serializable]
    public class LevelData
    {
        public int level = 1;
        [Range(2, 12)] public int pairs = 4;
        [Range(5f, 60f)] public float timePerPair = 20f;
    }

    [Header("main")]
    [Min(1)] public int minLevel = 1;
    [Min(1)] public int maxLevel = 8;

    [Header("levels")]
    public List<LevelData> levels = new List<LevelData>
    {
        new LevelData { level = 1, pairs = 2, timePerPair = 5f },
        new LevelData { level = 2, pairs = 3, timePerPair = 8f },
        new LevelData { level = 3, pairs = 4, timePerPair = 12f },
        new LevelData { level = 4, pairs = 5, timePerPair = 18f },
        new LevelData { level = 5, pairs = 6, timePerPair = 25f },
        new LevelData { level = 6, pairs = 7, timePerPair = 32f },
        new LevelData { level = 7, pairs = 8, timePerPair = 38f },
        new LevelData { level = 8, pairs = 9, timePerPair = 40f }
    };

    [Header("additional")]
    public int defaultPairsIfEmpty = 4;
    public float defaultTimeIfEmpty = 20f;
    public string defaultPlayerName = "Guest";

    public int GetPairsForLevel(int level)
    {
        var data = levels.Find(l => l.level == level);
        return data != null ? data.pairs : defaultPairsIfEmpty;
    }

    public float GetTimeForLevel(int level)
    {
        var data = levels.Find(l => l.level == level);
        return data != null ? data.timePerPair : defaultTimeIfEmpty;
    }

    public int GetLevelCount() => Mathf.Max(maxLevel, levels.Count);
}