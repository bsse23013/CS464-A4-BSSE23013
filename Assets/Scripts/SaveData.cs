using UnityEngine;

// the only script that spells the save keys
public static class SaveData
{
    const string UnlockedKey = "unlocked";
    const int LastLevel = 5;

    static string BestKey(int level) => "best" + level;

    // highest level that can be played, level 1 by default
    public static int Unlocked => PlayerPrefs.GetInt(UnlockedKey, 1);

    public static bool IsUnlocked(int level) => level <= Unlocked;

    public static void Unlock(int level)
    {
        level = Mathf.Min(level, LastLevel);
        if (level <= Unlocked) return;
        PlayerPrefs.SetInt(UnlockedKey, level);
        PlayerPrefs.Save();
    }

    public static bool HasBest(int level) => PlayerPrefs.HasKey(BestKey(level));

    public static float GetBest(int level) => PlayerPrefs.GetFloat(BestKey(level), 0f);

    // returns true when this is a new best time
    public static bool Record(int level, float time)
    {
        if (HasBest(level) && time >= GetBest(level)) return false;
        PlayerPrefs.SetFloat(BestKey(level), time);
        PlayerPrefs.Save();
        return true;
    }
}
