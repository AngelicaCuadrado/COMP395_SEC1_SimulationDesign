using UnityEngine;

public static class SaveManager
{
    private const string KEY_HAS_SAVE = "hasSave";
    private const string KEY_FOOD     = "food";
    private const string KEY_MERCURY  = "mercury";

    public static bool HasSave()
    {
        return PlayerPrefs.GetInt(KEY_HAS_SAVE, 0) == 1;
    }

    public static void SaveGame(int food, int mercury)
    {
        PlayerPrefs.SetInt(KEY_HAS_SAVE, 1);
        PlayerPrefs.SetInt(KEY_FOOD,     food);
        PlayerPrefs.SetInt(KEY_MERCURY,  mercury);
        PlayerPrefs.Save();
    }

    public static int GetFood()    { return PlayerPrefs.GetInt(KEY_FOOD, 0); }
    public static int GetMercury() { return PlayerPrefs.GetInt(KEY_MERCURY, 0); }

    // --- Per-Level Stars ---
    public static void SaveLevelStars(int level, int stars)
    {
        PlayerPrefs.SetInt("stars_level_" + level, stars);
        PlayerPrefs.Save();
    }
    public static int GetLevelStars(int level)
    {
        return PlayerPrefs.GetInt("stars_level_" + level, 0);
    }

    // --- Per-Level Stats ---
    public static void SaveLevelStats(int level, int foodCollected, int foodNeeded,
                                                  int mercCollected, int mercNeeded,
                                                  int levelScore)
    {
        PlayerPrefs.SetInt("food_collected_" + level, foodCollected);
        PlayerPrefs.SetInt("food_needed_"    + level, foodNeeded);
        PlayerPrefs.SetInt("merc_collected_" + level, mercCollected);
        PlayerPrefs.SetInt("merc_needed_"    + level, mercNeeded);
        PlayerPrefs.SetInt("level_score_"    + level, levelScore);
        PlayerPrefs.Save();
    }

    public static int GetLevelFoodCollected(int level) { return PlayerPrefs.GetInt("food_collected_" + level, 0); }
    public static int GetLevelFoodNeeded(int level)    { return PlayerPrefs.GetInt("food_needed_"    + level, 0); }
    public static int GetLevelMercCollected(int level) { return PlayerPrefs.GetInt("merc_collected_" + level, 0); }
    public static int GetLevelMercNeeded(int level)    { return PlayerPrefs.GetInt("merc_needed_"    + level, 0); }
    public static int GetLevelScore(int level)         { return PlayerPrefs.GetInt("level_score_"    + level, 0); }

    // --- Cumulative Score (across all levels) ---
    public static void AddToCumulativeScore(int points)
    {
        int current = GetCumulativeScore();
        PlayerPrefs.SetInt("cumulative_score", current + points);
        PlayerPrefs.Save();
    }
    public static int GetCumulativeScore() { return PlayerPrefs.GetInt("cumulative_score", 0); }

    public static void ClearSave()
    {
        PlayerPrefs.DeleteKey(KEY_HAS_SAVE);
        PlayerPrefs.DeleteKey(KEY_FOOD);
        PlayerPrefs.DeleteKey(KEY_MERCURY);
        PlayerPrefs.DeleteKey("cumulative_score");
        for (int i = 1; i <= 3; i++)
        {
            PlayerPrefs.DeleteKey("stars_level_"    + i);
            PlayerPrefs.DeleteKey("food_collected_" + i);
            PlayerPrefs.DeleteKey("food_needed_"    + i);
            PlayerPrefs.DeleteKey("merc_collected_" + i);
            PlayerPrefs.DeleteKey("merc_needed_"    + i);
            PlayerPrefs.DeleteKey("level_score_"    + i);
        }
        PlayerPrefs.Save();
    }


}
